using System.Text.Json.Serialization;

namespace MyExpensesAPI.Models.Account
{
    public record RefreshTokenRequest
    {
        public RefreshTokenRequest(string refreshToken)
        {
            RefreshToken = refreshToken;
        }

        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }
    }
}
