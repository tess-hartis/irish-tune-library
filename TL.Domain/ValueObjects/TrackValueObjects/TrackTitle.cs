using System.Text.RegularExpressions;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using TL.Domain.ValueObjects.TuneValueObjects;

namespace TL.Domain.ValueObjects.TrackValueObjects;

public record TrackTitle
{
    public readonly string Value;

    private TrackTitle(string value)
    {
        Value = value;
    }

    public static Validation<Error, TrackTitle> Create(string value)
    {
        var trimmed = value.Trim();
        
        if (string.IsNullOrWhiteSpace(trimmed))
            return Fail<Error, TrackTitle>("Track title cannot be empty");

        if (trimmed.Length < 5 && trimmed.Length > 0)
            return Fail<Error, TrackTitle>("Title is too short");

        if (trimmed.Length > 100)
            return Fail<Error, TrackTitle>("Title is too long");

        var pattern = @"^[a-zA-Z0-9\s]+$";
        
        if (!Regex.IsMatch(trimmed, pattern))
            return Fail<Error, TrackTitle>("Title contains invalid characters"); 
        
        return Success<Error, TrackTitle>(new TrackTitle(trimmed));
    }
}