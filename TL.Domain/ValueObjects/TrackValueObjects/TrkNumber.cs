using LanguageExt;
using static LanguageExt.Prelude;
using LanguageExt.Common;

namespace TL.Domain.ValueObjects.TrackValueObjects;

public record TrkNumber
{
    public int Value;

    private TrkNumber(int value)
    {
        Value = value;
    }

    public static Validation<Error, TrkNumber> Create(int value)
    {
        if (value < 1)
            return Fail<Error, TrkNumber>("Invalid track number");

        if (value > 50)
            return Fail<Error, TrkNumber>("Invalid track number");
        

        return Success<Error, TrkNumber>(new TrkNumber(value));
    }
}