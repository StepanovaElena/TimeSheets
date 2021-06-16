using FluentValidation;
using TimeSheets.Models.Dto.Requests;

namespace TimeSheets.Infrastructure.Validation
{
	public class ServiceRequestValidator : AbstractValidator<ServiceRequest>
	{
		public ServiceRequestValidator()
		{	
			RuleFor(x => x.Name).NotEmpty();
		}
	}
}
