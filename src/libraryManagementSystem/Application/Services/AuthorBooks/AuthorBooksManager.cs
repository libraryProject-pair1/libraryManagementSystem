using Application.Features.AuthorBooks.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.AuthorBooks;

public class AuthorBooksManager : IAuthorBooksService
{
    private readonly IAuthorBooksRepository _authorBooksRepository;
    private readonly AuthorBooksBusinessRules _authorBooksBusinessRules;

    public AuthorBooksManager(IAuthorBooksRepository authorBooksRepository, AuthorBooksBusinessRules authorBooksBusinessRules)
    {
        _authorBooksRepository = authorBooksRepository;
        _authorBooksBusinessRules = authorBooksBusinessRules;
    }

    public async Task<AuthorBook?> GetAsync(
        Expression<Func<AuthorBook, bool>> predicate,
        Func<IQueryable<AuthorBook>, IIncludableQueryable<AuthorBook, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        AuthorBook? authorBooks = await _authorBooksRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return authorBooks;
    }

    public async Task<IPaginate<AuthorBook>?> GetListAsync(
        Expression<Func<AuthorBook, bool>>? predicate = null,
        Func<IQueryable<AuthorBook>, IOrderedQueryable<AuthorBook>>? orderBy = null,
        Func<IQueryable<AuthorBook>, IIncludableQueryable<AuthorBook, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<AuthorBook> authorBooksList = await _authorBooksRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return authorBooksList;
    }

    public async Task<AuthorBook> AddAsync(AuthorBook authorBooks)
    {
        AuthorBook addedAuthorBooks = await _authorBooksRepository.AddAsync(authorBooks);

        return addedAuthorBooks;
    }

    public async Task<AuthorBook> UpdateAsync(AuthorBook authorBooks)
    {
        AuthorBook updatedAuthorBooks = await _authorBooksRepository.UpdateAsync(authorBooks);

        return updatedAuthorBooks;
    }

    public async Task<AuthorBook> DeleteAsync(AuthorBook authorBooks, bool permanent = false)
    {
        AuthorBook deletedAuthorBooks = await _authorBooksRepository.DeleteAsync(authorBooks);

        return deletedAuthorBooks;
    }
}
