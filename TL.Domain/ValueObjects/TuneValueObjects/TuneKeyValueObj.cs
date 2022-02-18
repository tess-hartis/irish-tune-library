using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace TL.Domain.ValueObjects.TuneValueObjects;

public record TuneKeyValueObj
{
    public readonly string Value;

    private TuneKeyValueObj(string value)
    {
        Value = value;
    }

    public static Validation<Error, TuneKeyValueObj> Create(string value)
    {
        var trimmed = value.Trim();

        if (!Enum.TryParse<TuneKeyEnum>(trimmed, true, out _))
            return Fail<Error, TuneKeyValueObj>("Invalid tune key");

        return Success<Error, TuneKeyValueObj>(new TuneKeyValueObj(trimmed));
    }
}