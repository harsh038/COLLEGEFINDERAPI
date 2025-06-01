using CollegeFinderAPI.Models;
using FluentValidation;

namespace CollegeFinderAPI.Validators
{
    public class CollegeCourseValidator : AbstractValidator<CollegeCourseModel>
    {
        public CollegeCourseValidator()
        {
            RuleFor(collegeCourse => collegeCourse.CollegeID)
                .GreaterThan(0).WithMessage("CollegeID must be greater than 0.");

            RuleFor(collegeCourse => collegeCourse.CourseID)
                .GreaterThan(0).WithMessage("CourseID must be greater than 0.");

            RuleFor(collegeCourse => collegeCourse.SeatAvailable)
                .GreaterThanOrEqualTo(0).WithMessage("SeatsAvailable must be greater than or equal to 0.");

            RuleFor(collegeCourse => collegeCourse.AdmissionCriteria)
                .NotEmpty().WithMessage("AdmissionCriteria is required.")
                .MaximumLength(1000).WithMessage("AdmissionCriteria cannot exceed 1000 characters.");

            RuleFor(collegeCourse => collegeCourse.Fee)
                .NotNull().WithMessage("Fee should not be null.")
                .NotEmpty().WithMessage("Fee should not be empty.")
                .Matches(@"^\d+$").WithMessage("Value must contain only numbers.");

        }
    }
}
