using System.Text.Json.Serialization;

namespace kamafi.auth.data.models
{
    public class TokenRequest
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("api_key")]
        public string ApiKey { get; set; }
    }
}
