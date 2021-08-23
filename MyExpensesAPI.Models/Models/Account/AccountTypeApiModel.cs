using System;
using System.Text.Json.Serialization;

namespace MyExpensesAPI.Models.Models.Account
{
    public record AccountTypeApiModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        public AccountTypeApiModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
