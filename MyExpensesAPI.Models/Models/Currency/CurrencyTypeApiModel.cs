using System;
using System.Text.Json.Serialization;

namespace MyExpensesAPI.Models.Models.Currency
{
    public record CurrencyTypeApiModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        public CurrencyTypeApiModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
