using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using kamafi.auth.data.extensions;
using System.IdentityModel.Tokens.Jwt;

namespace kamafi.auth.data.models
{
    public class Tenant : ITenant
    {
        private const string AuthHeader = "Authorization";

        [JsonPropertyName("actor")]
        public string Actor { get; set; }

        [JsonPropertyName("public_key")]
        public Guid? PublicKey { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("claims")]
        public Dictionary<string, string> Claims { get; set; } = new Dictionary<string, string>();

        [JsonIgnore]
        [JsonPropertyName("token")]
        public string Token { get; set; }

        public Tenant(IHttpContextAccessor httpContextAccessor)
        {
            var tenant = httpContextAccessor.HttpContext?.User;

            Actor = tenant?.FindFirst("actor")?.Value;
            PublicKey = tenant?.FindFirst("public_key")?.Value.ToNullableGuid();
            Role = tenant?.FindFirst("role")?.Value;

            foreach (var claim in tenant.Claims)
            {
                if (JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.ContainsKey(claim.Type))
                    Claims.Add(JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap[claim.Type], claim.Value);
                else
                    Claims.Add(claim.Type, claim.Value);
            }

            var request = httpContextAccessor.HttpContext?.Request;
            if (request != null &&
                request.Headers.ContainsKey(AuthHeader) &&
                request.Headers[AuthHeader].ToString().StartsWith(Keys.Bearer))
            {
                Token = request.Headers[AuthHeader].ToString().Replace($"{Keys.Bearer} ", string.Empty);
            }
        }
    }
}
