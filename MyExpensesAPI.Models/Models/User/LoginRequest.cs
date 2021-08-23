using System.Text.Json.Serialization;

namespace MyExpensesAPI.Models.User
{
    public record LoginRequest
    {
        public LoginRequest(string username, string password)
        {
            Username = username;
            Password = password;
        }

        [JsonPropertyName("username")] 
        public string Username { get; set; }

        [JsonPropertyName("password")] 
        public string Password { get; set; }
    }
}
