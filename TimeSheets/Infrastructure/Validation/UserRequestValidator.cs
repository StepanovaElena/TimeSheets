using FluentValidation;
using TimeSheets.Models.Dto;

namespace TimeSheets.Infrastructure.Validation
{
    public class UserRequestValidator : AbstractValidator<UserRequest>
    {
        private readonly string[] RequiredSymbols = { "@", "#", "$" };
        private readonly int MinimumPasswordLength = 8;

        public UserRequestValidator()
        {
            RuleFor(x => x.Username).NotEmpty();

            RuleFor(x => x.Password)
                .MinimumLength(MinimumPasswordLength);

            RuleFor(x => x.Password)
                .Must(IsContainsSubstring).WithMessage(ValidationsMessages.InvalidPassword);

            RuleFor(x => x.Role).NotEmpty();
        }

        private bool IsContainsSubstring(string password)
        {
            bool result = false;

            foreach (string symbol in RequiredSymbols)
            {
                result = result || password.Contains(symbol);
            }

            return result;
        }
    }
}
