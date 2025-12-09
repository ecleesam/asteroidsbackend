using asteroidsbackend.Data;
using asteroidsbackend.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace AsteroidsBackend.Tests.Services
{
    public class AuthServiceTests
    {
        private readonly AuthService _authService;
        private readonly AppDbContext _context;
        private readonly Mock<IConfiguration> _mockConfig;

        public AuthServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new AppDbContext(options);

            _mockConfig = new Mock<IConfiguration>();
            _mockConfig.Setup(c => c["Jwt:Key"]).Returns("ThisIsASecretKeyForAsteroidsBackendApi123!");
            _mockConfig.Setup(c => c["Jwt:Issuer"]).Returns("AsteroidsApi");
            _mockConfig.Setup(c => c["Jwt:Audience"]).Returns("AsteroidsClient");

            _authService = new AuthService(_context, _mockConfig.Object);
        }

        [Fact]
        public async Task RegisterAsync_ShouldCreateUser_WhenUserDoesNotExist()
        {
            // Arrange
            string username = "testuser";
            string password = "password123";

            // Act
            var result = await _authService.RegisterAsync(username, password);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(username, result.Username);
            Assert.NotEqual(password, result.PasswordHash); // Should be hashed
            Assert.Single(_context.Users);
        }

        [Fact]
        public async Task RegisterAsync_ShouldReturnNull_WhenUserAlreadyExists()
        {
            // Arrange
            string username = "testuser";
            string password = "password123";
            await _authService.RegisterAsync(username, password);

            // Act
            var result = await _authService.RegisterAsync(username, "newpassword");

            // Assert
            Assert.Null(result);
            Assert.Single(_context.Users);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnToken_WhenCredentialsAreCorrect()
        {
            // Arrange
            string username = "testuser";
            string password = "password123";
            await _authService.RegisterAsync(username, password);

            // Act
            var token = await _authService.LoginAsync(username, password);

            // Assert
            Assert.NotNull(token);
            Assert.NotEmpty(token);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnNull_WhenPasswordIsIncorrect()
        {
            // Arrange
            string username = "testuser";
            string password = "password123";
            await _authService.RegisterAsync(username, password);

            // Act
            var token = await _authService.LoginAsync(username, "wrongpassword");

            // Assert
            Assert.Null(token);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Act
            var token = await _authService.LoginAsync("nonexistent", "password");

            // Assert
            Assert.Null(token);
        }
    }
}
