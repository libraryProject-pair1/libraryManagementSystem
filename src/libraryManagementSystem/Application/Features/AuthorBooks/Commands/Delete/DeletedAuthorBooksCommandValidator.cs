using FluentValidation;

namespace Application.Features.AuthorBooks.Commands.Delete;

public class DeleteAuthorBooksCommandValidator : AbstractValidator<DeleteAuthorBooksCommand>
{
    public DeleteAuthorBooksCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}