using FluentValidation;

namespace Application.Features.Publishers.Commands.Create;

public class CreatePublisherCommandValidator : AbstractValidator<CreatePublisherCommand>
{
    public CreatePublisherCommandValidator()
    {
        RuleFor(c => c.PublisherName).NotEmpty();
        RuleFor(c => c.Adress).NotEmpty();
        RuleFor(c => c.Phone).NotEmpty();
    }
}