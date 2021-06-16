using FluentValidation;
using TimeSheets.Models.Dto.Requests;

namespace TimeSheets.Infrastructure.Validation
{
    public class ContractRequestValidator : AbstractValidator<ContractRequest>
    {
		public ContractRequestValidator()
		{
			RuleFor(x => x.Description)
				.NotEmpty();

			RuleFor(x => x.Title)
				.NotEmpty();

			RuleFor(x => x.DateStart)
				.LessThanOrEqualTo(x => x.DateEnd)
				.WithMessage(ValidationsMessages.RequestDateStartError);

			RuleFor(x => x.DateEnd)
				.GreaterThanOrEqualTo(x => x.DateStart)
				.WithMessage(ValidationsMessages.RequestDateEndError);

		}
	}
}
