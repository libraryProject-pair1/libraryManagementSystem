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

namespace Application.Features.CategoryBooks.Commands.Create;

public class CreateCategoryBookCommand : IRequest<CreatedCategoryBookResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid BookId { get; set; }
    public Guid CategoryId { get; set; }

    public string[] Roles => [Admin, Write, CategoryBooksOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetCategoryBooks"];

    public class CreateCategoryBookCommandHandler : IRequestHandler<CreateCategoryBookCommand, CreatedCategoryBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryBookRepository _categoryBookRepository;
        private readonly CategoryBookBusinessRules _categoryBookBusinessRules;

        public CreateCategoryBookCommandHandler(IMapper mapper, ICategoryBookRepository categoryBookRepository,
                                         CategoryBookBusinessRules categoryBookBusinessRules)
        {
            _mapper = mapper;
            _categoryBookRepository = categoryBookRepository;
            _categoryBookBusinessRules = categoryBookBusinessRules;
        }

        public async Task<CreatedCategoryBookResponse> Handle(CreateCategoryBookCommand request, CancellationToken cancellationToken)
        {
            CategoryBook categoryBook = _mapper.Map<CategoryBook>(request);

            await _categoryBookRepository.AddAsync(categoryBook);

            CreatedCategoryBookResponse response = _mapper.Map<CreatedCategoryBookResponse>(categoryBook);
            return response;
        }
    }
}