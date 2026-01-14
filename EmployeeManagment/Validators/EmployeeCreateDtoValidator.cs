using EmployeeManagment.DTOs.Employee;
using FluentValidation;

namespace EmployeeManagment.Validators
{
    public class EmployeeCreateDtoValidator: AbstractValidator<EmployeeCreateDto>
    {
        public EmployeeCreateDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotNull()
            .NotEmpty()
            .WithMessage("First name is required");

            RuleFor(x => x.LastName)
                  .NotNull()
            .NotEmpty()
            .WithMessage("LAst name is required");

            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress();
               

            RuleFor(x => x.DepartmentId)
                .NotNull()
                .NotEmpty()
            .WithMessage("Department ID is required");

            RuleFor(x => x.JobTitleId)
                .NotNull()
                .NotEmpty()
                .WithMessage("JobTitle ID is required");
        }
    }
}
