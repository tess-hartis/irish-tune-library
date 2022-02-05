namespace TL.Domain.ValueObjects.TuneValueObjects;

public record TuneComposer
{
    public readonly string Value;

    private TuneComposer(string value)
    {
        Value = value;
    }

    public static TuneComposer Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new Exception();

        return new TuneComposer(value);
    }
}