﻿using FluentValidation;
using MyExpensesAPI.Models.Account;

namespace MyExpensesAPI.Models.Validation.Account
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>, IValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotNull()
                .EmailAddress()
                .OverridePropertyName("email")
                .WithName("email");

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