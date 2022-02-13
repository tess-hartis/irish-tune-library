using System.Text.RegularExpressions;
using LanguageExt;
using static LanguageExt.Prelude;
using LanguageExt.Common;

namespace TL.Domain.ValueObjects.AlbumValueObjects;

public record AlbumTitle
{
    public readonly string Value;

    private AlbumTitle(string value)
    {
        Value = value;
    }

    public static Validation<Error, AlbumTitle> Create(string value)
    {
        var trimmed = value.Trim();
        
        if (string.IsNullOrWhiteSpace(trimmed))
            return Fail<Error, AlbumTitle>("Album title cannot be empty");

        if (trimmed.Length < 5 && trimmed.Length > 0)
            return Fail<Error, AlbumTitle>("Title is too short");

        if (trimmed.Length > 100)
            return Fail<Error, AlbumTitle>("Title is too long");

        var pattern = @"^[a-zA-Z0-9\s]+$";
        
        if (!Regex.IsMatch(trimmed, pattern))
            return Fail<Error, AlbumTitle>("Title contains invalid characters");
        
        return Success<Error, AlbumTitle>(new AlbumTitle(trimmed));
    }
}