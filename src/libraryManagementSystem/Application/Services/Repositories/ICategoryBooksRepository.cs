using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ICategoryBooksRepository : IAsyncRepository<CategoryBook, Guid>, IRepository<CategoryBook, Guid>
{
}