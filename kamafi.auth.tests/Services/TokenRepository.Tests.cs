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

namespace kamafi.auth.tests.services
{
    [Trait("category", "UnitTest")]
    public class TokenRepository
    {
        private readonly User _defaultUser;
        private readonly ITokenRepository<User> _userTokenRepo;

        public TokenRepository()
        {
            _defaultUser = Helper.DefaultUser;
            _userTokenRepo = Helper.GetUserTokenRepository();
        }

        [Fact]
        public void BuildClaims_WithDefaultUser_ShouldBeSuccessful()
        {
            // Arrange & Act
            var claims = _userTokenRepo.BuildClaims(_defaultUser);
            var actorClaim = claims.FirstOrDefault(x => x.Type == "actor");
            var publicKeyClaim = claims.FirstOrDefault(x => x.Type == "public_key");

            // Assert
            claims.Should().NotBeEmpty();
            actorClaim.Should().NotBeNull();
            actorClaim.Value.Should().Be("user");
            publicKeyClaim.Should().NotBeNull();
            publicKeyClaim.Value.Should().Be(_defaultUser.PublicKey.ToString());
        }

        [Fact]
        public void BuildToken_WithDefaultUser_ShouldBeSuccessful()
        {
            // Arrange & Act
            var tokenResponse = _userTokenRepo.BuildToken(_defaultUser);

            // Assert
            tokenResponse.Should().NotBeNull();
            tokenResponse.ExpiresIn.Should().BeGreaterThanOrEqualTo(0);
            tokenResponse.AccessToken.Should().NotBeNullOrWhiteSpace();
        }

        //[Theory]
        //[InlineData("test")]
        //[InlineData("password")]
        //[InlineData("j&dVf6GJYMRUyn")]
        //public void Hash_WithValidInput_ShouldBeSuccessful(string input)
        //{
        //    // Arrange & Act
        //    var hashedInput = _userTokenRepo.Hash(input);
        //
        //    // Assert
        //    hashedInput.Should().NotBeNullOrWhiteSpace();
        //    hashedInput.Length.Should().BeGreaterThan(0);
        //}
    }
}
