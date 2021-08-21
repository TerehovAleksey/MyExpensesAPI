using System;
using System.Text.Json.Serialization;

namespace MyExpensesAPI.Models.Models.Currency
{
    public record CurrencyCreateApiModel
    {
        [JsonPropertyName("typeId")]
        public Guid CurrencyTypeId { get; set; }

        [JsonPropertyName("rate")]
        public decimal Rate { get; set; }

        [JsonPropertyName("default")]
        public bool IsDefault { get; set; }
    }
}
