using NArchitecture.Core.Application.Responses;

namespace Application.Features.AuthorBooks.Commands.Delete;

public class DeletedAuthorBooksResponse : IResponse
{
    public Guid Id { get; set; }
}