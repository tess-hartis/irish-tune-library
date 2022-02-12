using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using TL.Domain.ValueObjects.TuneValueObjects;

namespace TL.Domain.ValueObjects.TrackValueObjects;

public record TrackTitle
{
    public readonly string Value;

    private TrackTitle(string value)
    {
        Value = value;
    }

    public static Validation<Error, TrackTitle> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Fail<Error, TrackTitle>("Track title cannot be empty");

        return Success<Error, TrackTitle>(new TrackTitle(value));
    }
}