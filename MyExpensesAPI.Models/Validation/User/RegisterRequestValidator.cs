using FluentValidation;
using MyExpensesAPI.Models.User;

namespace MyExpensesAPI.Models.Validation.User
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>, IValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .OverridePropertyName("email");

            RuleFor(x => x.UserName)
                .NotEmpty()
                .MinimumLength(6)
                .OverridePropertyName("username")
                .WithName(Strings.Username);

            RuleFor(x => x.Password)
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
