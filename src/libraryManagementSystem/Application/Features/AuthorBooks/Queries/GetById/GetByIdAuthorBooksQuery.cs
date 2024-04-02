using Application.Features.AuthorBooks.Constants;
using Application.Features.AuthorBooks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.AuthorBooks.Constants.AuthorBooksOperationClaims;

namespace Application.Features.AuthorBooks.Queries.GetById;

public class GetByIdAuthorBooksQuery : IRequest<GetByIdAuthorBooksResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdAuthorBooksQueryHandler : IRequestHandler<GetByIdAuthorBooksQuery, GetByIdAuthorBooksResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAuthorBooksRepository _authorBooksRepository;
        private readonly AuthorBooksBusinessRules _authorBooksBusinessRules;

        public GetByIdAuthorBooksQueryHandler(IMapper mapper, IAuthorBooksRepository authorBooksRepository, AuthorBooksBusinessRules authorBooksBusinessRules)
        {
            _mapper = mapper;
            _authorBooksRepository = authorBooksRepository;
            _authorBooksBusinessRules = authorBooksBusinessRules;
        }

        public async Task<GetByIdAuthorBooksResponse> Handle(GetByIdAuthorBooksQuery request, CancellationToken cancellationToken)
        {
            AuthorBook? authorBooks = await _authorBooksRepository.GetAsync(predicate: ab => ab.Id == request.Id, cancellationToken: cancellationToken);
            await _authorBooksBusinessRules.AuthorBooksShouldExistWhenSelected(authorBooks);

            GetByIdAuthorBooksResponse response = _mapper.Map<GetByIdAuthorBooksResponse>(authorBooks);
            return response;
        }
    }
}