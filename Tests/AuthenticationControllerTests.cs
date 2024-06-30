using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;
using WebAPI.Configuration;
using WebAPI.Dto;
using Microsoft.AspNetCore.Identity;
using FluentAssertions;
using System.Text;
using JWT.Builder;
using JWT.Algorithms;
using Core.Entity;
using Microsoft.Extensions.Configuration;

namespace WebApi.Tests.Controllers
{
    public class AuthenticationControllerTests
    {
        private readonly Mock<UserManager<UserEntity>> _mockUserManager;
        private readonly Mock<ILogger<AuthenticationController>> _mockLogger;
        private readonly JwtSettings _jwtSettings;
        private readonly AuthenticationController _controller;

        public AuthenticationControllerTests()
        {
            _mockUserManager = MockUserManager<UserEntity>();
            _mockLogger = new Mock<ILogger<AuthenticationController>>();
            _jwtSettings = new JwtSettings(new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string>
            {
                { "JwtSettings:ValidIssuer", "mipie" },
                { "JwtSettings:ValidAudience", "mipie" },
                { "JwtSettings:Secret", "IUbH8zDkLea58S3UllVuswtYQ3oxmFbC9" }
            }).Build());

            _controller = new AuthenticationController(_mockUserManager.Object, _mockLogger.Object, null, _jwtSettings);
        }

        [Fact]
        public async Task Authenticate_ShouldReturnToken_WhenCredentialsAreValid()
        {
            // Arrange
            var loginDto = new LoginUserDto { LoginName = "demo", Password = "Alfonso1234@" };
            var user = new UserEntity { UserName = "demo", Email = "Dawid@o2.pl" };

            _mockUserManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(user);
            _mockUserManager.Setup(x => x.CheckPasswordAsync(It.IsAny<UserEntity>(), It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await _controller.Authenticate(loginDto);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();

            var tokenResponse = okResult.Value as AuthenticationResponseDto;
            tokenResponse.Should().NotBeNull();
            tokenResponse.Token.Should().StartWith("Bearer ");
        }

        [Fact]
        public async Task Authenticate_ShouldReturnUnauthorized_WhenCredentialsAreInvalid()
        {
            // Arrange
            var loginDto = new LoginUserDto { LoginName = "demo", Password = "wrongpassword" };
            var user = new UserEntity { UserName = "demo", Email = "Dawid@o2.pl" };

            _mockUserManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(user);
            _mockUserManager.Setup(x => x.CheckPasswordAsync(It.IsAny<UserEntity>(), It.IsAny<string>())).ReturnsAsync(false);

            // Act
            var result = await _controller.Authenticate(loginDto);

            // Assert
            result.Should().BeOfType<UnauthorizedResult>();
        }

        private static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            return mgr;
        }
    }
}
