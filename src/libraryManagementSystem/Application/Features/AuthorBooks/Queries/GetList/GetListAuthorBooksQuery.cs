using Application.Features.AuthorBooks.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.AuthorBooks.Constants.AuthorBooksOperationClaims;

namespace Application.Features.AuthorBooks.Queries.GetList;

public class GetListAuthorBooksQuery : IRequest<GetListResponse<GetListAuthorBooksListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListAuthorBooks({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetAuthorBooks";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListAuthorBooksQueryHandler : IRequestHandler<GetListAuthorBooksQuery, GetListResponse<GetListAuthorBooksListItemDto>>
    {
        private readonly IAuthorBooksRepository _authorBooksRepository;
        private readonly IMapper _mapper;

        public GetListAuthorBooksQueryHandler(IAuthorBooksRepository authorBooksRepository, IMapper mapper)
        {
            _authorBooksRepository = authorBooksRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListAuthorBooksListItemDto>> Handle(GetListAuthorBooksQuery request, CancellationToken cancellationToken)
        {
            IPaginate<AuthorBook> authorBooks = await _authorBooksRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListAuthorBooksListItemDto> response = _mapper.Map<GetListResponse<GetListAuthorBooksListItemDto>>(authorBooks);
            return response;
        }
    }
}