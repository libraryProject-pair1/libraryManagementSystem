using Application.Features.AuthorBooks.Commands.Create;
using Application.Features.AuthorBooks.Commands.Delete;
using Application.Features.AuthorBooks.Commands.Update;
using Application.Features.AuthorBooks.Queries.GetById;
using Application.Features.AuthorBooks.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.AuthorBooks.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<AuthorBook, CreateAuthorBooksCommand>().ReverseMap();
        CreateMap<AuthorBook, CreatedAuthorBooksResponse>().ReverseMap();
        CreateMap<AuthorBook, UpdateAuthorBooksCommand>().ReverseMap();
        CreateMap<AuthorBook, UpdatedAuthorBooksResponse>().ReverseMap();
        CreateMap<AuthorBook, DeleteAuthorBooksCommand>().ReverseMap();
        CreateMap<AuthorBook, DeletedAuthorBooksResponse>().ReverseMap();
        CreateMap<AuthorBook, GetByIdAuthorBooksResponse>().ReverseMap();
        CreateMap<AuthorBook, GetListAuthorBooksListItemDto>().ReverseMap();
        CreateMap<IPaginate<AuthorBook>, GetListResponse<GetListAuthorBooksListItemDto>>().ReverseMap();
    }
}