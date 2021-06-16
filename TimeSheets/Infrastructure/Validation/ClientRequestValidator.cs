using FluentValidation;
using TimeSheets.Models.Dto.Requests;

namespace TimeSheets.Infrastructure.Validation
{
    public class ClientRequestValidator : AbstractValidator<ClientRequest>
    {
        public ClientRequestValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
