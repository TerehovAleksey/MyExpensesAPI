using FluentValidation;
using MyExpensesAPI.Models.Models.Currency;

namespace MyExpensesAPI.Models.Validation.Currency
{
    public class CurrencyCreateApiModelValidator : AbstractValidator<CurrencyCreateApiModel>, IValidator<CurrencyCreateApiModel>
    {
        public CurrencyCreateApiModelValidator()
        {
            RuleFor(x => x.CurrencyTypeId)
                .NotNull()
                .NotEmpty()
                .OverridePropertyName("typeId")
                .SetValidator(new GuidValidator());

            RuleFor(x => x.Rate)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .OverridePropertyName("rate")
                .WithName(Strings.Rate);

            RuleFor(x => x.IsDefault)
                .NotNull()
                .OverridePropertyName("default");
        }
    }
}
