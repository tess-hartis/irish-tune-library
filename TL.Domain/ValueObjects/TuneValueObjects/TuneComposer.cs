using System.Text.RegularExpressions;
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
        var trimmed = value.Trim();
        
        if (string.IsNullOrWhiteSpace(trimmed))
            return Fail<Error, TuneComposer>("Composer name cannot be empty");
        
        if (trimmed.Length < 5 && trimmed.Length > 0)
            return Fail<Error, TuneComposer>("Composer name is too short");

        if (trimmed.Length > 100)
            return Fail<Error, TuneComposer>("Composer name is too long");

        var pattern = @"^[a-zA-Z0-9\s]+$";
        
        if (!Regex.IsMatch(trimmed, pattern))
            return Fail<Error, TuneComposer>("Composer name contains invalid characters");
        
        return Success<Error, TuneComposer>(new TuneComposer(trimmed));
    }
}