using kamafi.auth.data.models;

namespace kamafi.auth.services
{
    public interface IUserRepository
    {
        Task<User> GetAsync(int userId);
        Task<User> GetAsync(string apiKey);
        Task<TokenResponse> GetTokenAsync(TokenRequest request);
        Task<User> AddAsync(UserDto dto);
        string HashPassword(string password, byte[] salt);
        bool VerifyPassword(User user, string password);
    }
}
