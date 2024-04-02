using Application.Features.CategoryBooks.Constants;
using Application.Features.CategoryBooks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.CategoryBooks.Constants.CategoryBooksOperationClaims;

namespace Application.Features.CategoryBooks.Commands.Update;

public class UpdateCategoryBookCommand : IRequest<UpdatedCategoryBookResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid CategoryId { get; set; }

    public string[] Roles => [Admin, Write, CategoryBooksOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetCategoryBooks"];

    public class UpdateCategoryBookCommandHandler : IRequestHandler<UpdateCategoryBookCommand, UpdatedCategoryBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryBookRepository _categoryBookRepository;
        private readonly CategoryBookBusinessRules _categoryBookBusinessRules;

        public UpdateCategoryBookCommandHandler(IMapper mapper, ICategoryBookRepository categoryBookRepository,
                                         CategoryBookBusinessRules categoryBookBusinessRules)
        {
            _mapper = mapper;
            _categoryBookRepository = categoryBookRepository;
            _categoryBookBusinessRules = categoryBookBusinessRules;
        }

        public async Task<UpdatedCategoryBookResponse> Handle(UpdateCategoryBookCommand request, CancellationToken cancellationToken)
        {
            CategoryBook? categoryBook = await _categoryBookRepository.GetAsync(predicate: cb => cb.Id == request.Id, cancellationToken: cancellationToken);
            await _categoryBookBusinessRules.CategoryBookShouldExistWhenSelected(categoryBook);
            categoryBook = _mapper.Map(request, categoryBook);

            await _categoryBookRepository.UpdateAsync(categoryBook!);

            UpdatedCategoryBookResponse response = _mapper.Map<UpdatedCategoryBookResponse>(categoryBook);
            return response;
        }
    }
}