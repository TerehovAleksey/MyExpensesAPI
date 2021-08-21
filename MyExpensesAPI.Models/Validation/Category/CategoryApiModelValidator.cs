using FluentValidation;
using MyExpensesAPI.Models.Models.Category;

namespace MyExpensesAPI.Models.Validation.Category
{
    public class CategoryApiModelValidator : AbstractValidator<CategoryApiModel>, IValidator<CategoryApiModel>
    {
        public CategoryApiModelValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .OverridePropertyName("id")
                .SetValidator(new GuidValidator());

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(30)
                .OverridePropertyName("name")
                .WithName(Strings.Name);
        }
    }
}
