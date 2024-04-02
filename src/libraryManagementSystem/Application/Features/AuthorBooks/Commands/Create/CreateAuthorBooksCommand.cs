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

namespace Application.Features.AuthorBooks.Commands.Create;

public class CreateAuthorBooksCommand : IRequest<CreatedAuthorBooksResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid AuthorId { get; set; }
    public Guid BookId { get; set; }

    public string[] Roles => [Admin, Write, AuthorBooksOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetAuthorBooks"];

    public class CreateAuthorBooksCommandHandler : IRequestHandler<CreateAuthorBooksCommand, CreatedAuthorBooksResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAuthorBooksRepository _authorBooksRepository;
        private readonly AuthorBooksBusinessRules _authorBooksBusinessRules;

        public CreateAuthorBooksCommandHandler(IMapper mapper, IAuthorBooksRepository authorBooksRepository,
                                         AuthorBooksBusinessRules authorBooksBusinessRules)
        {
            _mapper = mapper;
            _authorBooksRepository = authorBooksRepository;
            _authorBooksBusinessRules = authorBooksBusinessRules;
        }

        public async Task<CreatedAuthorBooksResponse> Handle(CreateAuthorBooksCommand request, CancellationToken cancellationToken)
        {
            AuthorBook authorBooks = _mapper.Map<AuthorBook>(request);

            await _authorBooksRepository.AddAsync(authorBooks);

            CreatedAuthorBooksResponse response = _mapper.Map<CreatedAuthorBooksResponse>(authorBooks);
            return response;
        }
    }
}