using Application.Features.CategoryBooks.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.CategoryBooks;

public class CategoryBooksManager : ICategoryBooksService
{
    private readonly ICategoryBooksRepository _categoryBooksRepository;
    private readonly CategoryBookBusinessRules _categoryBooksBusinessRules;

    public CategoryBooksManager(ICategoryBooksRepository categoryBooksRepository, CategoryBookBusinessRules categoryBooksBusinessRules)
    {
        _categoryBooksRepository = categoryBooksRepository;
        _categoryBooksBusinessRules = categoryBooksBusinessRules;
    }

    public async Task<CategoryBook?> GetAsync(
        Expression<Func<CategoryBook, bool>> predicate,
        Func<IQueryable<CategoryBook>, IIncludableQueryable<CategoryBook, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        CategoryBook? categoryBooks = await _categoryBooksRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return categoryBooks;
    }

    public async Task<IPaginate<CategoryBook>?> GetListAsync(
        Expression<Func<CategoryBook, bool>>? predicate = null,
        Func<IQueryable<CategoryBook>, IOrderedQueryable<CategoryBook>>? orderBy = null,
        Func<IQueryable<CategoryBook>, IIncludableQueryable<CategoryBook, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<CategoryBook> categoryBooksList = await _categoryBooksRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return categoryBooksList;
    }

    public async Task<CategoryBook> AddAsync(CategoryBook categoryBooks)
    {
        CategoryBook addedCategoryBooks = await _categoryBooksRepository.AddAsync(categoryBooks);

        return addedCategoryBooks;
    }

    public async Task<CategoryBook> UpdateAsync(CategoryBook categoryBooks)
    {
        CategoryBook updatedCategoryBooks = await _categoryBooksRepository.UpdateAsync(categoryBooks);

        return updatedCategoryBooks;
    }

    public async Task<CategoryBook> DeleteAsync(CategoryBook categoryBooks, bool permanent = false)
    {
        CategoryBook deletedCategoryBooks = await _categoryBooksRepository.DeleteAsync(categoryBooks);

        return deletedCategoryBooks;
    }
}
