using FluentValidation;
using MyExpensesAPI.Models.Models.Account;

namespace MyExpensesAPI.Models.Validation.Account
{
    public class AccountCreateApiModelValidator : AbstractValidator<AccountCreateApiModel>, IValidator<AccountCreateApiModel>
    {
        public AccountCreateApiModelValidator()
        {
            RuleFor(x => x.AccountTypeId)
                .NotNull()
                .NotEmpty()
                .OverridePropertyName("typeId")
                .SetValidator(new GuidValidator());

            RuleFor(x => x.UserCurrencyId)
                .NotNull()
                .NotEmpty()
                .OverridePropertyName("currencyId")
                .SetValidator(new GuidValidator());

            RuleFor(x => x.Balance)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(0)
                .OverridePropertyName("balance")
                .WithName(Strings.Balance);
        }
    }
}
