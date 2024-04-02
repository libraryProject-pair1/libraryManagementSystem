using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IAuthorBooksRepository : IAsyncRepository<AuthorBook, Guid>, IRepository<AuthorBook, Guid>
{
}