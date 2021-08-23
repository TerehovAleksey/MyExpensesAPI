using System;
using System.Text.Json.Serialization;

namespace MyExpensesAPI.Models.Models.Account
{
    public record AccountCreateApiModel
    {
        [JsonPropertyName("typeId")]
        public Guid AccountTypeId { get; set; }

        [JsonPropertyName("currencyId")]
        public Guid UserCurrencyId { get; set; }

        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
    }
}
