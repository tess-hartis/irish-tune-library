using LanguageExt;
using static LanguageExt.Prelude;
using LanguageExt.Common;

namespace TL.Domain.ValueObjects.TrackTuneValueObjects;

public record TrackTuneOrder
{
    public readonly int Value;

    private TrackTuneOrder(int value)
    {
        Value = value;
    }

    public static Validation<Error, TrackTuneOrder> Create(int value)
    {
        if (value < 1 || value > 25)
            return Fail<Error, TrackTuneOrder>("Invalid order");

        return Success<Error, TrackTuneOrder>(new TrackTuneOrder(value));
    }
}