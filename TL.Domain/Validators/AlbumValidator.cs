// using FluentValidation;
//
// namespace TL.Domain.Validators;
//
// public class AlbumValidator : AbstractValidator<Album>
// {
//     public AlbumValidator()
//     {
//         RuleFor(x => x.Title)
//             .NotEmpty().WithMessage("title cannot be empty")
//             .Length(2, 75).WithMessage("title must be between 2 and 75 characters");
//         RuleFor(x => x.Year)
//             .NotEmpty().WithMessage("year cannot be empty")
//             .Must(BeValidYear).WithMessage("invalid year");
//     }
//
//     private bool BeValidYear(int year)
//     {
//         if (year > DateTime.Now.Year)
//             return false;
//
//         if (year < 1900)
//             return false;
//
//         return true;
//     }
// }