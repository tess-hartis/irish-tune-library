namespace TL.Domain.ValueObjects.TrackValueObjects;

public record TrackNumber
{
    public readonly int Value;

    private TrackNumber(int value)
    {
        Value = value;
    }

    public static TrackNumber Create(int value)
    {
        if (value < 1 || value > 50)
            throw new Exception();

        return new TrackNumber(value);
    }
}