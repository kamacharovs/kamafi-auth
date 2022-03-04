using System;
using System.Linq;
using System.Threading.Tasks;
using kamafi.auth.data;
using kamafi.auth.data.models;
using kamafi.auth.services;
using Xunit;
using NSubstitute;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace kamafi.auth.tests.services
{
    public class UserRepositoryTests
    {
        private readonly IUserRepository _repo;

        public UserRepositoryTests()
        {
            _repo = Helper.GetUserRepository();
        }

        [Fact]
        public async Task GetAsync_WithApiKey_ShouldBeSuccessful()
        {
            // Arrange & Act
            var user = await _repo.GetAsync(Helper.DefaultUserApiKey.ApiKey);

            // Assert
            user.Should().NotBeNull();
            user.UserId.Should().Be(Helper.DefaultUser.UserId);
            user.PublicKey.Should().Be(Helper.DefaultUser.PublicKey);
            user.FirstName.Should().Be(Helper.DefaultUser.FirstName);
            user.LastName.Should().Be(Helper.DefaultUser.LastName);
            user.Email.Should().Be(Helper.DefaultUser.Email);
            user.Password.Should().Be(Helper.DefaultUser.Password);
            user.PasswordSalt.Should().Be(Helper.DefaultUser.PasswordSalt);
            user.RoleName.Should().Be(Helper.DefaultUser.RoleName);
            user.Created.Should().Be(Helper.DefaultUser.Created);
            user.Updated.Should().Be(Helper.DefaultUser.Updated);
            user.IsDeleted.Should().Be(Helper.DefaultUser.IsDeleted);
        }
    }
}
