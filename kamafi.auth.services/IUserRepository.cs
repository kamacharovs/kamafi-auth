using kamafi.auth.data.models;

namespace kamafi.auth.services
{
    public interface IUserRepository
    {
        Task<User> GetAsync();
        Task<User> GetAsync(int userId);
        Task<User> GetAsync(string apiKey);
        Task<TokenResponse> GetTokenAsync(TokenRequest request);
        Task<User> AddAsync(UserDto dto);
        Task<UserApiKey> AddApiKeyAsync(int? userId = null, bool isEnabled = true);
        string HashPassword(string password, byte[] salt);
        bool VerifyPassword(User user, string password);
    }
}
