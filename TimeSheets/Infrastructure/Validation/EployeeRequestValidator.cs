using FluentValidation;
using TimeSheets.Models.Dto;

namespace TimeSheets.Infrastructure.Validation
{
    public class EmployeeRequestValidator : AbstractValidator<EmployeeRequest>
    {
        public EmployeeRequestValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
