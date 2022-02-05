using Microsoft.VisualBasic.CompilerServices;

namespace TL.Domain.ValueObjects.TuneValueObjects;

public record TuneTitle
{
    public readonly string Value;

    private TuneTitle(string value)
    {
        Value = value;
    }

    public static TuneTitle Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new Exception();

        return new TuneTitle(value);
    }
}