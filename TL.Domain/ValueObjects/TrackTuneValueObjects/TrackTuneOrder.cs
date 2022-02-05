namespace TL.Domain.ValueObjects.TrackTuneValueObjects;

public record TrackTuneOrder
{
    public readonly int Value;

    private TrackTuneOrder(int value)
    {
        Value = value;
    }

    public static TrackTuneOrder Create(int value)
    {
        if (value < 1 || value > 25)
            throw new Exception();

        return new TrackTuneOrder(value);
    }
}