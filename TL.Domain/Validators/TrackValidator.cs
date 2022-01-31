using FluentValidation;

namespace TL.Domain.Validators;

public class TrackValidator : AbstractValidator<Track>
{
    public TrackValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("title cannot be empty")
            .Length(2, 75).WithMessage("title must be between 2 and 75 characters");
        RuleFor(x => x.TrackNumber)
            .NotEmpty().WithMessage("track number cannot be empty")
            .Must(BeValidTrackNumber).WithMessage("invalid track number");

    }

    private bool BeValidTrackNumber(int trackNumber)
    {
        if (trackNumber < 1)
            return false;

        if (trackNumber > 99)
            return false;

        return true;
    }
}