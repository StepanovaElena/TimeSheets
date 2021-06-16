using FluentValidation;
using TimeSheets.Models.Dto.Requests;

namespace TimeSheets.Infrastructure.Validation
{	
	public class InvoiceRequestValidator : AbstractValidator<InvoiceRequest>
	{
		public InvoiceRequestValidator()
		{
			RuleFor(x => x.ContractId)
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
