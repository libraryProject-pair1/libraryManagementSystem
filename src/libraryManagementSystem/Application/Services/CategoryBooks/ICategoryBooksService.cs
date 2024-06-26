using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.CategoryBooks;

public interface ICategoryBooksService
{
    Task<CategoryBook?> GetAsync(
        Expression<Func<CategoryBook, bool>> predicate,
        Func<IQueryable<CategoryBook>, IIncludableQueryable<CategoryBook, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<CategoryBook>?> GetListAsync(
        Expression<Func<CategoryBook, bool>>? predicate = null,
        Func<IQueryable<CategoryBook>, IOrderedQueryable<CategoryBook>>? orderBy = null,
        Func<IQueryable<CategoryBook>, IIncludableQueryable<CategoryBook, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<CategoryBook> AddAsync(CategoryBook categoryBooks);
    Task<CategoryBook> UpdateAsync(CategoryBook categoryBooks);
    Task<CategoryBook> DeleteAsync(CategoryBook categoryBooks, bool permanent = false);
}
