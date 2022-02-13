using LanguageExt;
using static LanguageExt.Prelude;
using LanguageExt.Common;

namespace TL.Domain.ValueObjects.TrackValueObjects;

public record TrackNumber
{
    public int Value;

    private TrackNumber(int value)
    {
        Value = value;
    }

    public static Validation<Error, TrackNumber> Create(int value)
    {
        if (value < 1)
            return Fail<Error, TrackNumber>("Invalid track number");

        if (value > 50)
            return Fail<Error, TrackNumber>("Invalid track number");
        

        return Success<Error, TrackNumber>(new TrackNumber(value));
    }
}