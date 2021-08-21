using System;
using System.Text.Json.Serialization;

namespace MyExpensesAPI.Models.Models.Category
{
    public record CategoryApiModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        public CategoryApiModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
