namespace TL.Domain.ValueObjects.TrackValueObjects;

public record TrackNumber
{
    public int Value;

    private TrackNumber(int value)
    {
        Value = value;
    }

    public static TrackNumber Create(int value)
    {
        
        return new TrackNumber(value);
    }
}