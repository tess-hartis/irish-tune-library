using LanguageExt;
using static LanguageExt.Prelude;
using LanguageExt.Common;

namespace TL.Domain.ValueObjects.AlbumValueObjects;

public record AlbumYear
{
    public readonly int Value;

    private AlbumYear(int value)
    {
        Value = value;
    }

    public static Validation<Error, AlbumYear> Create(int value)
    {
        if (value > DateTime.Now.Year)
            return Fail<Error, AlbumYear>("Invalid year");

        return Success<Error, AlbumYear>(new AlbumYear(value));
    }
}