using FluentValidation;

namespace Application.Features.AuthorBooks.Commands.Update;

public class UpdateAuthorBooksCommandValidator : AbstractValidator<UpdateAuthorBooksCommand>
{
    public UpdateAuthorBooksCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.AuthorId).NotEmpty();
        RuleFor(c => c.BookId).NotEmpty();
    }
}