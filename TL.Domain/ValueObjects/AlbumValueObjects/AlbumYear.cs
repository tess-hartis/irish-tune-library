namespace TL.Domain.ValueObjects.AlbumValueObjects;

public record AlbumYear
{
    public readonly int Value;

    private AlbumYear(int value)
    {
        Value = value;
    }

    public static AlbumYear Create(int value)
    {
        if (value > DateTime.Now.Year)
            throw new Exception();

        return new AlbumYear(value);
    }
}