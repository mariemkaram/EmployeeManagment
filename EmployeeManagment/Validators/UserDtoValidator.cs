using EmployeeManagment.DTOs.User;
using FluentValidation;

namespace EmployeeManagment.Validators
{
    public class UserDtoValidator:AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(r => r.Email)
                .NotNull()
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress();

            RuleFor(r => r.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("Password is required")
                .MinimumLength(6);
        }
    }
}
