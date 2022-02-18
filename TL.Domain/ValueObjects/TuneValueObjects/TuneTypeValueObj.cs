using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace TL.Domain.ValueObjects.TuneValueObjects;

public record TuneTypeValueObj
{
    public readonly string Value;

    private TuneTypeValueObj(string value)
    {
        Value = value;
    }

    public static Validation<Error, TuneTypeValueObj> Create(string value)
    {
        var trimmed = value.Trim();
        
        if (!Enum.TryParse<TuneTypeEnum>(trimmed, true, out _))
            return Fail<Error, TuneTypeValueObj>("Invalid tune type");

        return Success<Error, TuneTypeValueObj>(new TuneTypeValueObj(trimmed));

    }
}