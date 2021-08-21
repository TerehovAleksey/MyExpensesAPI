using System;
using System.Text.Json.Serialization;

namespace MyExpensesAPI.Models.Models.Currency
{
    public record CurrencyApiModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("rate")]
        public decimal Rate { get; set; }

        [JsonPropertyName("default")]
        public bool Default { get; set; }

        [JsonPropertyName("type")]
        public CurrencyTypeApiModel Type { get; set; }

        public CurrencyApiModel(Guid id, decimal rate, bool isDefault, CurrencyTypeApiModel type)
        {
            Id = id;
            Rate = rate;
            Default = isDefault;
            Type = type;
        }

        public CurrencyApiModel()
        {

        }
    }
}
