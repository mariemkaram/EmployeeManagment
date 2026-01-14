using EmployeeManagment.DTOs.JobTitle;
using FluentValidation;

namespace EmployeeManagment.Validators
{
    public class JobTitleValidator: AbstractValidator<JobTitleDto>
    {
        public JobTitleValidator()
        {
            RuleFor(x => x.Name)
            .NotNull()
           .NotEmpty()
           .WithMessage("This field is required");
        }
       
    }
}
