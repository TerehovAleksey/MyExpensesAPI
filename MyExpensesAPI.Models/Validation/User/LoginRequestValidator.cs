using FluentValidation;
using MyExpensesAPI.Models.User;

namespace MyExpensesAPI.Models.Validation.User
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>, IValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Username)
                .NotNull()
                .OverridePropertyName("username")
                .WithName(Strings.Username);

            RuleFor(x => x.Password)
                .NotNull()
                .MinimumLength(8)
                .Matches("[A-Z]")
                .Matches("[a-z]")
                .Matches("[0-9]")
                .Matches("[^a-zA-Z0-9]")
                .OverridePropertyName("password")
                .WithName(Strings.Password);
        }
    }
}
