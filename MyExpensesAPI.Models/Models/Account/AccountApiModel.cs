using MyExpensesAPI.Models.Models.Currency;
using System;
using System.Text.Json.Serialization;

namespace MyExpensesAPI.Models.Models.Account
{
    public record AccountApiModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("type")]
        public AccountTypeApiModel AccountType { get; set; }

        [JsonPropertyName("currency")]
        public CurrencyApiModel UserCurrency { get; set; }

        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
    }
}
