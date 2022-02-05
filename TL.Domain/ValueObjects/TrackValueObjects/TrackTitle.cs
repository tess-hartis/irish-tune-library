namespace TL.Domain.ValueObjects.TrackValueObjects;

public record TrackTitle
{
    public readonly string Value;

    private TrackTitle(string value)
    {
        Value = value;
    }

    public static TrackTitle Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new Exception();

        return new TrackTitle(value);
    }
}