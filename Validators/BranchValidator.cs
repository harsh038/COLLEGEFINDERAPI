using CollegeFinderAPI.Models;
using FluentValidation;

namespace CollegeFinderAPI.Validators
{
    public class BranchValidator : AbstractValidator<BranchModel>  
    {
        public BranchValidator()
        {
            RuleFor(branch => branch.CourseID)
                .GreaterThan(0).WithMessage("CourseID must be greater than 0.");

            RuleFor(branch => branch.BranchName)  
                .NotEmpty().WithMessage("BranchName is required.")
                .MaximumLength(200).WithMessage("BranchName cannot exceed 200 characters.");

            RuleFor(branch => branch.Content)
                .NotEmpty().WithMessage("Content is required.")
                .MaximumLength(5000).WithMessage("Content cannot exceed 5000 characters.");

            RuleFor(branch => branch.About)
                .NotEmpty().WithMessage("About is required.")
                .MaximumLength(5000).WithMessage("Content cannot exceed 5000 characters.");
        }
    }
}
