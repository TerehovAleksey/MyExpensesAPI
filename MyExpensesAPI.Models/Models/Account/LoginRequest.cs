using System.Text.Json.Serialization;

namespace MyExpensesAPI.Models.Account
{
    public record LoginRequest
    {
        public LoginRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }

        [JsonPropertyName("email")] 
        public string Email { get; set; }

        [JsonPropertyName("password")] 
        public string Password { get; set; }
    }
}
