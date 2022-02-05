namespace TL.Domain.ValueObjects.AlbumValueObjects;

public record AlbumTitle
{
    public readonly string Value;

    private AlbumTitle(string value)
    {
        Value = value;
    }

    public static AlbumTitle Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new Exception();

        return new AlbumTitle(value);
    }
}