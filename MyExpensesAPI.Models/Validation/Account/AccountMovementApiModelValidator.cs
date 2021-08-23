using FluentValidation;
using MyExpensesAPI.Models.Models.Account;

namespace MyExpensesAPI.Models.Validation.Account
{
    public class AccountMovementApiModelValidator : AbstractValidator<AccountMovementApiModel>, IValidator<AccountMovementApiModel>
    {
        public AccountMovementApiModelValidator()
        {
            RuleFor(x => x.CategoryId)
                .NotNull()
                .NotEmpty()
                .OverridePropertyName("categoryId")
                .SetValidator(new GuidValidator());

            RuleFor(x => x.Description)
                .MaximumLength(250)
                .OverridePropertyName("description")
                .WithName(Strings.Description);

            RuleFor(x => x.IsRefill)
                .NotNull()
                .NotEmpty()
                .OverridePropertyName("isRefill");

            RuleFor(x => x.UserAccountId)
                .NotNull()
                .NotEmpty()
                .OverridePropertyName("accountId")
                .SetValidator(new GuidValidator());

            RuleFor(x=>x.Value)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(0)
                .OverridePropertyName("value")
                .WithName(Strings.Value);
        }
    }
}
