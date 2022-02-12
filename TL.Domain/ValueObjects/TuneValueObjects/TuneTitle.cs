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
        if (string.IsNullOrWhiteSpace(value))
            return Fail<Error, TuneTitle>("coo itb");

        return Success<Error, TuneTitle>(new TuneTitle(value));
    }
}

// public static Either<List<string>, TuneTitle> Create(string value)
    // {
    //     var errors = new List<string>();
    //     
    //     if(string.IsNullOrWhiteSpace(value))
    //         errors.Add("cannot exceed 100 char");
    //
    //     if (errors.Any())
    //         return Left<List<string>, TuneTitle>(errors);
    //
    //     return Right<List<string>, TuneTitle>(new TuneTitle(value));
    //     
    // }

    // public static Validation<Error, TuneTitle> Validate(string title)
    // {
    //     var titleV = MaxStrLength(100)(title);
    //     return (titleV).Apply((t) => new TuneTitle(title));
    // }
    //
    // public static Func<string, Validation<Error, string>> MaxStrLength(int max) =>
    //     s => s.Length <= max
    //         ? Success<Error, string>(s)
    //         : Fail<Error, string>(Error.New("exceeds max length"));

//     public static Validation<Error, string> NonEmpty(string str) =>
//         String.IsNullOrEmpty(str)
//             ? Validation<Error, String>.Fail(Seq<Error>().Add(Error.New("Non empty string is required")))
//             : Validation<Error, String>.Success(str);
//     
// }
//
// public class Error : NewType<Error, string>
// {
//     public Error(string e) : base(e) { }
// }