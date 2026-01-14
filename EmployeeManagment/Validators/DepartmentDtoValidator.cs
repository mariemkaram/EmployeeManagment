using EmployeeManagment.DTOs.Department;
using FluentValidation;

namespace EmployeeManagment.Validators
{
    public class DepartmentDtoValidator: AbstractValidator<DepartmentDto>
    {
        public DepartmentDtoValidator()
        {
            RuleFor(x => x.Name)
              .NotNull()
             .NotEmpty()
             .WithMessage("This field is required");
        }
    }
}
