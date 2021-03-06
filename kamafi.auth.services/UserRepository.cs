using System.Security.Cryptography;
using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using kamafi.auth.data;
using kamafi.auth.data.models;
using kamafi.auth.data.exceptions;
using kamafi.auth.data.extensions;

namespace kamafi.auth.services
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly ITenant _tenant;
        private readonly ITokenRepository<User> _tokenRepo;
        private readonly AuthDbContext _context;

        private const KeyDerivationPrf _prf = KeyDerivationPrf.HMACSHA256;
        private const int _iterationCount = 100000;
        private const int _numBytesRequested = 256 / 8;

        public UserRepository(
            ILogger<UserRepository> logger,
            ITenant tenant,
            ITokenRepository<User> tokenRepo,
            AuthDbContext context)
        {
            _logger = logger;
            _tenant = tenant;
            _tokenRepo = tokenRepo;
            _context = context;
        }

        public async Task<User> GetAsync()
        {
            return await _context.Users
                .FirstAsync(x => x.PublicKey == _tenant.PublicKey);
        }

        public async Task<User> GetAsync(int userId)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.UserId == userId)
                ?? throw new AuthNotFoundException();
        }

        public async Task<User> GetAsync(string apiKey)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.UserId ==
                    x.ApiKeys.First(x => x.ApiKey == apiKey).UserId)
                ?? throw new AuthNotFoundException();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Email == email)
                ?? throw new AuthNotFoundException();
        }

        public async Task<User> GetAsync(string email, string password)
        {
            var user = await GetUserByEmailAsync(email);

            if (VerifyPassword(user, password) is false)
                throw new AuthFriendlyException(HttpStatusCode.BadRequest, "Incorrect email or password");

            return user;
        }

        public async Task<TokenResponse> GetTokenAsync(TokenRequest request)
        {
            var user = request.ApiKey is null
                ? await GetAsync(request.Email, request.Password)
                : await GetAsync(request.ApiKey);

            return _tokenRepo.BuildToken(user);
        }

        public async Task<User> AddAsync(UserDto dto)
        {
            var salt = new byte[128 / 8];
            RandomNumberGenerator.Create().GetNonZeroBytes(salt);

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Password = HashPassword(dto.Password, salt),
                PasswordSalt = Convert.ToBase64String(salt),
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Added User with UserId={UserId}", user.UserId);

            if (dto.AddApiKey)
            {
                await AddApiKeyAsync(user.UserId);
            }

            return user;
        }

        public async Task<UserApiKey> AddApiKeyAsync(int? userId = null, bool isEnabled = true)
        {
            if (userId is null)
            {
                var user = await GetAsync();
                userId = user.UserId;
            }

            var apiKey = AuthExtensions.GenerateApiKey();
            var userApiKey = new UserApiKey
            {
                UserId = userId.GetValueOrDefault(),
                ApiKey = apiKey,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
                IsEnabled = isEnabled
            };

            await _context.UserApiKeys.AddAsync(userApiKey);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Added User.ApiKey for UserId={UserId}", userApiKey.UserId);

            return userApiKey;
        }

        // Source: https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-6.0
        public string HashPassword(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: _prf,
                iterationCount: _iterationCount,
                numBytesRequested: _numBytesRequested));
        }

        public bool VerifyPassword(User user, string password)
        {
            var hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Convert.FromBase64String(user.PasswordSalt),
                prf: _prf,
                iterationCount: _iterationCount,
                numBytesRequested: _numBytesRequested));

            return hashedPassword == user.Password;
        }
    }
}