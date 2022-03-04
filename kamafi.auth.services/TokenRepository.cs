using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using kamafi.auth.data;
using kamafi.auth.data.models;

namespace kamafi.auth.services
{
    public class TokenRepository<T> : ITokenRepository<T> where T : class, IEntity
    {
        private readonly ILogger<TokenRepository<T>> _logger;
        private readonly IConfiguration _config;

        public TokenRepository(
            ILogger<TokenRepository<T>> logger,
            IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public IEnumerable<Claim> BuildClaims(T entity)
        {
            return new[]
            {
                new Claim("actor", typeof(T).Name.ToLowerInvariant()),
                new Claim("public_key", entity.PublicKey.ToString()),
                new Claim("role", entity.RoleName)
            };
        }

        public TokenResponse BuildToken(T entity)
        {
            var claims = BuildClaims(entity);
            var expires = Convert.ToInt32(_config[Keys.JwtExpires]);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config[Keys.JwtKey]));
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(expires),
                Issuer = _config[Keys.JwtIssuer],
                Audience = _config[Keys.JwtAudience],
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new TokenResponse
            {
                ExpiresIn = expires,
                AccessToken = tokenHandler.WriteToken(token)
            };
        }
    }
}
