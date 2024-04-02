using Application.Features.AuthorBooks.Constants;
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

namespace Application.Features.AuthorBooks.Commands.Delete;

public class DeleteAuthorBooksCommand : IRequest<DeletedAuthorBooksResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, AuthorBooksOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetAuthorBooks"];

    public class DeleteAuthorBooksCommandHandler : IRequestHandler<DeleteAuthorBooksCommand, DeletedAuthorBooksResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAuthorBooksRepository _authorBooksRepository;
        private readonly AuthorBooksBusinessRules _authorBooksBusinessRules;

        public DeleteAuthorBooksCommandHandler(IMapper mapper, IAuthorBooksRepository authorBooksRepository,
                                         AuthorBooksBusinessRules authorBooksBusinessRules)
        {
            _mapper = mapper;
            _authorBooksRepository = authorBooksRepository;
            _authorBooksBusinessRules = authorBooksBusinessRules;
        }

        public async Task<DeletedAuthorBooksResponse> Handle(DeleteAuthorBooksCommand request, CancellationToken cancellationToken)
        {
            AuthorBook? authorBooks = await _authorBooksRepository.GetAsync(predicate: ab => ab.Id == request.Id, cancellationToken: cancellationToken);
            await _authorBooksBusinessRules.AuthorBooksShouldExistWhenSelected(authorBooks);

            await _authorBooksRepository.DeleteAsync(authorBooks!);

            DeletedAuthorBooksResponse response = _mapper.Map<DeletedAuthorBooksResponse>(authorBooks);
            return response;
        }
    }
}