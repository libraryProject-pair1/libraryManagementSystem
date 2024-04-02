using Application.Features.AuthorBooks.Constants;
using Application.Features.AuthorBooks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.AuthorBooks.Constants.AuthorBooksOperationClaims;

namespace Application.Features.AuthorBooks.Commands.Update;

public class UpdateAuthorBooksCommand : IRequest<UpdatedAuthorBooksResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public Guid BookId { get; set; }

    public string[] Roles => [Admin, Write, AuthorBooksOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetAuthorBooks"];

    public class UpdateAuthorBooksCommandHandler : IRequestHandler<UpdateAuthorBooksCommand, UpdatedAuthorBooksResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAuthorBooksRepository _authorBooksRepository;
        private readonly AuthorBooksBusinessRules _authorBooksBusinessRules;

        public UpdateAuthorBooksCommandHandler(IMapper mapper, IAuthorBooksRepository authorBooksRepository,
                                         AuthorBooksBusinessRules authorBooksBusinessRules)
        {
            _mapper = mapper;
            _authorBooksRepository = authorBooksRepository;
            _authorBooksBusinessRules = authorBooksBusinessRules;
        }

        public async Task<UpdatedAuthorBooksResponse> Handle(UpdateAuthorBooksCommand request, CancellationToken cancellationToken)
        {
            AuthorBook? authorBooks = await _authorBooksRepository.GetAsync(predicate: ab => ab.Id == request.Id, cancellationToken: cancellationToken);
            await _authorBooksBusinessRules.AuthorBooksShouldExistWhenSelected(authorBooks);
            authorBooks = _mapper.Map(request, authorBooks);

            await _authorBooksRepository.UpdateAsync(authorBooks!);

            UpdatedAuthorBooksResponse response = _mapper.Map<UpdatedAuthorBooksResponse>(authorBooks);
            return response;
        }
    }
}