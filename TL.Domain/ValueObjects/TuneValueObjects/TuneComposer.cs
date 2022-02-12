using LanguageExt;
using static LanguageExt.Prelude;
using LanguageExt.Common;

namespace TL.Domain.ValueObjects.TuneValueObjects;

public record TuneComposer
{
    public readonly string Value;

    private TuneComposer(string value)
    {
        Value = value;
    }

    public static Validation<Error, TuneComposer> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Fail<Error, TuneComposer>("nope lol");


        return Success<Error, TuneComposer>(new TuneComposer(value));
    }
}