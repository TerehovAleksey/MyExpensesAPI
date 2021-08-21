using FluentValidation;
using System;

namespace MyExpensesAPI.Models.Validation
{
    public class GuidValidator : AbstractValidator<Guid>
    {
        public GuidValidator()
        {
            RuleFor(x => x).NotEmpty();
        }
    }
}
