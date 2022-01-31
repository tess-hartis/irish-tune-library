using FluentValidation;

namespace TL.Domain.Validators;

public class ArtistValidator : AbstractValidator<Artist>
{
    public ArtistValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("name cannot be empty")
            .Length(2, 50).WithMessage("name must be between 2 and 50 characters");
    }
}