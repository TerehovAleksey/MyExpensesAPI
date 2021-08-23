using System.Text.Json.Serialization;
using System;

namespace MyExpensesAPI.Models.Models.Journal
{
    public record JournalRecordApiModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("date")]
        public DateTime DateTime { get; set; }

        [JsonPropertyName("category")]
        public string CategoryName { get; set; }

        [JsonPropertyName("value")]
        public decimal Value { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
