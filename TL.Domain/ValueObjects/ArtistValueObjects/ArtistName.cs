using System.Linq.Expressions;
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
        if (string.IsNullOrWhiteSpace(value))
            return Fail<Error, ArtistName>("Artist name cannot be empty");

        return Success<Error, ArtistName>(new ArtistName(value));
    }
}