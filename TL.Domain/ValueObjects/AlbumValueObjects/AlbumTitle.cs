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
        if (string.IsNullOrWhiteSpace(value))
            return Fail<Error, AlbumTitle>("Album title cannot be empty");

        return Success<Error, AlbumTitle>(new AlbumTitle(value));
    }
}