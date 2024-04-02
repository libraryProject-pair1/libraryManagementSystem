using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class AuthorBooksRepository : EfRepositoryBase<AuthorBook, Guid, BaseDbContext>, IAuthorBooksRepository
{
    public AuthorBooksRepository(BaseDbContext context) : base(context)
    {
    }
}