using FluentValidation;

namespace Application.Features.AuthorBooks.Commands.Create;

public class CreateAuthorBooksCommandValidator : AbstractValidator<CreateAuthorBooksCommand>
{
    public CreateAuthorBooksCommandValidator()
    {
        RuleFor(c => c.AuthorId).NotEmpty();
        RuleFor(c => c.BookId).NotEmpty();
    }
}