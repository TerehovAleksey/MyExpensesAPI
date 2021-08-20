using System.Text.Json.Serialization;

namespace MyExpensesAPI.Models.Account
{
    public record RegisterRequest
    {
        public RegisterRequest(string email, string userName, string password)
        {
            Email = email;
            UserName = userName;
            Password = password;
        }

        [JsonPropertyName("email")] 
        public string Email { get; set; }

        [JsonPropertyName("username")] 
        public string UserName { get; set; }

        [JsonPropertyName("password")] 
        public string Password { get; set; }
    }
}
