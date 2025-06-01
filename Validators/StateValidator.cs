using CollegeFinderAPI.Models;
using FluentValidation;

namespace CollegeFinderAPI.Validators
{
    public class StateValidator : AbstractValidator<StateModel>
    {
        public StateValidator()
        {
            RuleFor(state => state.CountryID)
                .GreaterThan(0).WithMessage("CountryID must be greater than 0.");

            RuleFor(state => state.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
        }
    }
}