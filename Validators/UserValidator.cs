using CollegeFinderAPI.Models;
using FluentValidation;

namespace CollegeFinderAPI.Validators
{
    public class UserValidator : AbstractValidator<UserModel>
    {
        public UserValidator()
        {
            RuleFor(user => user.FirstName)
                .NotEmpty().WithMessage("First Name is required.")
                .MaximumLength(100).WithMessage("First Name cannot exceed 100 characters.");

            RuleFor(user => user.LastName)
                .NotEmpty().WithMessage("Last Name is required.")
                .MaximumLength(100).WithMessage("Last Name cannot exceed 100 characters.");

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid Email format.")
                .MaximumLength(200).WithMessage("Email cannot exceed 200 characters.");

            RuleFor(user => user.PasswordHash)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .MaximumLength(30).WithMessage("Password cannot exceed 30 characters.");

            RuleFor(user => user.Role)
                .Must(role => string.IsNullOrEmpty(role) || role == "Admin" || role == "User")
                .WithMessage("Role must be either 'Admin' or 'User' if provided.");

        }
    }
}
