using System.Linq.Expressions;

namespace TL.Domain.ValueObjects.ArtistValueObjects;

public record ArtistName
{
    public readonly string Value;

    private ArtistName(string value)
    {
        Value = value;
    }

    public static ArtistName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new Exception();

        return new ArtistName(value);
    }
}