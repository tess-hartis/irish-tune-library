using System.Linq.Expressions;
using System.Text.RegularExpressions;
using LanguageExt;
using static LanguageExt.Prelude;
using LanguageExt.Common;

namespace TL.Domain.ValueObjects.ArtistValueObjects;

public record ArtistName
{
    public readonly string Value;

    private ArtistName(string value)
    {
        Value = value;
    }

    public static Validation<Error, ArtistName> Create(string value)
    {
        var trimmed = value.Trim();
        
        if (string.IsNullOrWhiteSpace(trimmed))
            return Fail<Error, ArtistName>("Artist name cannot be empty");

        if (trimmed.Length < 5 && trimmed.Length > 0)
            return Fail<Error, ArtistName>("Name is too short");

        if (trimmed.Length > 100)
            return Fail<Error, ArtistName>("Name is too long");

        var pattern = @"^[a-zA-Z0-9\s]+$";
        
        if (!Regex.IsMatch(trimmed, pattern))
            return Fail<Error, ArtistName>("Name contains invalid characters");
        
        return Success<Error, ArtistName>(new ArtistName(trimmed));
    }
}