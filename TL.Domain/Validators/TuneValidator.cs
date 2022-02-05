// using FluentValidation;
//
// namespace TL.Domain.Validators;
//
// public class TuneValidator : AbstractValidator<Tune>
// {
//     public TuneValidator()
//     {
//         RuleFor(x => x.Title)
//             .NotEmpty().WithMessage("title cannot be empty")
//             .Length(2, 75).WithMessage("title must be between 2 and 75 characters");
//         RuleFor(x => x.Composer)
//             .NotEmpty().WithMessage("composer cannot be empty")
//             .Length(2, 75).WithMessage("composer must be between 2 and 75 characters");
//
//     }
//
//     
// }