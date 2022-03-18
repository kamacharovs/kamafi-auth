using System;
using System.Linq;
using kamafi.auth.data;
using kamafi.auth.data.models;
using kamafi.auth.services;
using Xunit;
using NSubstitute;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace kamafi.auth.tests
{
    public static class Helper
    {
        public static User DefaultUser = new User
        {
            UserId = 1,
            PublicKey = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@gmail.com",
            Password = "1234",
            PasswordSalt = "salt",
            RoleName = Roles.Admin,
            Created = DateTime.UtcNow.AddDays(-1),
            Updated = DateTime.UtcNow.AddDays(-1),
            IsDeleted = false
        };

        public static UserApiKey DefaultUserApiKey = new UserApiKey
        {
            UserId = 1,
            ApiKey = "api-key",
            Created = DateTime.UtcNow.AddDays(-1),
            Updated = DateTime.UtcNow.AddDays(-1),
            IsEnabled = true
        };

        public static AuthDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<AuthDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new AuthDbContext(options);

            context.Users.Add(DefaultUser);
            context.UserApiKeys.Add(DefaultUserApiKey);
            context.SaveChanges();

            return context;
        }
    
        public static IConfiguration GetConfiguration()
        {
            var config = Substitute.For<IConfiguration>();

            config[Keys.JwtExpires].Returns(900.ToString());
            config[Keys.JwtIssuer].Returns("kamafi.auth");
            config[Keys.JwtAudience].Returns("kamafi.auth.audience");
            config[Keys.JwtKey].Returns("HauRhiWhyrfh0PivCVsovh1rJlQs36Aj");

            return config;
        }

        public static ITenant GetTenant(
            Guid? publicKey = null,
            string role = null)
        {
            var tenant = Substitute.For<ITenant>();

            tenant.Actor.Returns("user");
            tenant.PublicKey.Returns(publicKey ?? Guid.NewGuid());
            tenant.Role.Returns(role ?? "user");
            tenant.Claims.Returns(new System.Collections.Generic.Dictionary<string, string>
            {
                { "actor", "user" }
            });
            tenant.Token.Returns("token");

            return tenant;
        }

        public static ITokenRepository<User> GetUserTokenRepository()
        {
            return new TokenRepository<User>(
                logger: Substitute.For<ILogger<TokenRepository<User>>>(),
                config: GetConfiguration());
        }

        public static IUserRepository GetUserRepository()
        {
            return new UserRepository(
                logger: Substitute.For<ILogger<UserRepository>>(),
                tenant: GetTenant(),
                tokenRepo: GetUserTokenRepository(),
                context: GetContext());
        }
    }
}
