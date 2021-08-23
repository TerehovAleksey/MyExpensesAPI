using System.Text.Json.Serialization;

namespace MyExpensesAPI.Models.User
{
    public record LoginResult
    {
        public LoginResult(string userName, string role, string accessToken,
            string refreshToken)
        {
            UserName = userName;
            Role = role;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        [JsonPropertyName("username")] 
        public string UserName { get; set; }

        [JsonPropertyName("role")] 
        public string Role { get; set; }

        [JsonPropertyName("accessToken")] 
        public string AccessToken { get; set; }

        [JsonPropertyName("refreshToken")] 
        public string RefreshToken { get; set; }
    }
}
