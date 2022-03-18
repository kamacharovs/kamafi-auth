using System.Text.Json.Serialization;

namespace kamafi.auth.data.models
{
    public interface ITenant
    {
        [JsonPropertyName("actor")]
        public string Actor { get; set; }

        [JsonPropertyName("public_key")]
        public Guid? PublicKey { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("claims")]
        Dictionary<string, string> Claims { get; set; }

        [JsonIgnore]
        [JsonPropertyName("token")]
        string Token { get; set; }
    }
}
