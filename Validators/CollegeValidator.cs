using CollegeFinderAPI.Models;
using FluentValidation;

namespace CollegeFinderAPI.Validators
{
    public class CollegeValidator : AbstractValidator<CollegeModel>
    {
        public CollegeValidator()
        {
            RuleFor(college => college.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(200).WithMessage("Name cannot exceed 200 characters.");

            RuleFor(college => college.CityID)
                .GreaterThan(0).WithMessage("CityID must be greater than 0.");

            RuleFor(college => college.Type)
                .NotEmpty().WithMessage("Type is required.")
                .MaximumLength(100).WithMessage("Type cannot exceed 100 characters.");

            RuleFor(college => college.EstablishmentYear)
                .GreaterThan(1800).WithMessage("Establishment year must be after 1800.")
                .LessThanOrEqualTo(DateTime.Now.Year).WithMessage("Establishment year cannot be in the future.");

            RuleFor(college => college.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(500).WithMessage("Address cannot exceed 500 characters.");

            RuleFor(college => college.Website)
     .NotEmpty().WithMessage("Website is required.")
     .MaximumLength(200).WithMessage("Website cannot exceed 200 characters.")
     .Must(url =>
     {
         return Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult)
             && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
     })
     .WithMessage("Website must be a valid URL starting with http:// or https://");



            RuleFor(college => college.Description)
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.")
                .When(college => !string.IsNullOrEmpty(college.Description));
        }
    }
}

