using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class CategoryBooksRepository : EfRepositoryBase<CategoryBook, Guid, BaseDbContext>, ICategoryBooksRepository
{
    public CategoryBooksRepository(BaseDbContext context) : base(context)
    {
    }
}