using System;
using System.Text.Json.Serialization;

namespace MyExpensesAPI.Models.Models.Account
{
    public record AccountMovementApiModel
    {
        [JsonPropertyName("accountId")]
        public Guid UserAccountId { get; set; }

        [JsonPropertyName("categoryId")]
        public Guid CategoryId { get; set; }

        [JsonPropertyName("value")]
        public decimal Value { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("isRefill")]
        public bool IsRefill { get; set; }
    }
}
