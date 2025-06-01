using CollegeFinderAPI.Models;
using FluentValidation;

namespace CollegeFinderAPI.Validators
{
    public class CourseValidator : AbstractValidator<CourseModel>
    {
        public CourseValidator()
        {
            RuleFor(course => course.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(200).WithMessage("Name cannot exceed 200 characters.");

            RuleFor(course => course.Duration)
                .NotEmpty().WithMessage("Duration is required.")
                .MaximumLength(100).WithMessage("Duration cannot exceed 100 characters.");
        }
    }
}
