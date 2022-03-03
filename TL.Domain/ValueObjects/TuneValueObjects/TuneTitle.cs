using System.Text.RegularExpressions;
using LanguageExt;
using static LanguageExt.Prelude;
using LanguageExt.Common;

namespace TL.Domain.ValueObjects.TuneValueObjects;

public record TuneTitle
{
    public readonly string Value;

    private TuneTitle(string value)
    {
        Value = value;
    }

    public static Validation<Error, TuneTitle> Create(string value)
    {
        var trimmed = value.Trim();
        
        if (string.IsNullOrWhiteSpace(trimmed))
            return Fail<Error, TuneTitle>("Title cannot be empty");

        if (trimmed.Length < 5 && trimmed.Length > 0)
            return Fail<Error, TuneTitle>("Title is too short");

        if (trimmed.Length > 100)
            return Fail<Error, TuneTitle>("Title is too long");

        var pattern = @"^[a-zA-Z0-9\s]+$";
        
        if (!Regex.IsMatch(trimmed, pattern))
            return Fail<Error, TuneTitle>("Title contains invalid characters");
        
        return Success<Error, TuneTitle>(new TuneTitle(trimmed));
    }
}

