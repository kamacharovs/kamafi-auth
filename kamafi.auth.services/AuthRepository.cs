using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using kamafi.auth.data;

namespace kamafi.auth.services
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ILogger<AuthRepository> _logger;
        private readonly IConfiguration _config;
        private readonly AuthDbContext _context;

        public AuthRepository(
            ILogger<AuthRepository> logger,
            IConfiguration config,
            AuthDbContext context)
        {
            _logger = logger;
            _config = config;
            _context = context;
        }
    }
}