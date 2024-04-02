using Application.Features.AuthorBooks.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.AuthorBooks.Rules;

public class AuthorBooksBusinessRules : BaseBusinessRules
{
    private readonly IAuthorBooksRepository _authorBooksRepository;
    private readonly ILocalizationService _localizationService;

    public AuthorBooksBusinessRules(IAuthorBooksRepository authorBooksRepository, ILocalizationService localizationService)
    {
        _authorBooksRepository = authorBooksRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, AuthorBooksBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task AuthorBooksShouldExistWhenSelected(AuthorBook? authorBooks)
    {
        if (authorBooks == null)
            await throwBusinessException(AuthorBooksBusinessMessages.AuthorBooksNotExists);
    }

    public async Task AuthorBooksIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        AuthorBook? authorBooks = await _authorBooksRepository.GetAsync(
            predicate: ab => ab.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await AuthorBooksShouldExistWhenSelected(authorBooks);
    }
}