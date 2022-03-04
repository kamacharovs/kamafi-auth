using kamafi.auth.data.models;
using System.Security.Claims;

namespace kamafi.auth.services
{
    public interface ITokenRepository<T> where T : class, IEntity
    {
        IEnumerable<Claim> BuildClaims(T entity);
        TokenResponse BuildToken(T entity);
    }
}