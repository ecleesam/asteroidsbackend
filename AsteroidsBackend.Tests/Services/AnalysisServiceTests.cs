using Xunit;
using Moq;
using asteroidsbackend.Services;
using asteroidsbackend.Data;
using asteroidsbackend.Models;
using asteroidsbackend.Models.Interfaces;

namespace AsteroidsBackend.Tests.Services
{
    public class AnalysisServiceTests
    {
        private readonly Mock<IItemRepository> _mockRepo;
        private readonly AnalysisService _service;

        public AnalysisServiceTests()
        {
            // Arrange: Setup common mocks
            _mockRepo = new Mock<IItemRepository>();
            _service = new AnalysisService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetCategoryCounts_ShouldReturnCorrectCounts()
        {
            // Arrange
            var testData = new List<IItem>
            {
                new Weapon { Category = Category.Weapon },
                new Weapon { Category = Category.Weapon },
                new PowerUp { Category = Category.PowerUp }
            };

            _mockRepo.Setup(repo => repo.GetAllAsync())
                     .ReturnsAsync(testData);

            // Act
            var result = await _service.GetCategoryCountsAsync();

            // Assert
            Assert.Equal(2, result[Category.Weapon]);
            Assert.Equal(1, result[Category.PowerUp]);
        }

        [Fact]
        public async Task GetCategoryCounts_EmptyRepository_ShouldReturnEmpty()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetAllAsync())
                     .ReturnsAsync(new List<IItem>());

            // Act
            var result = await _service.GetCategoryCountsAsync();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetTop5Weapons_ShouldReturnStrongestWeapons_Sorted()
        {
            // Arrange
            var testData = new List<IItem>
            {
                new Weapon { Name = "Weak", Damage = 10 },
                new Weapon { Name = "Strong", Damage = 100 },
                new Weapon { Name = "Medium", Damage = 50 },
                new PowerUp { Name = "NotAWeapon" } // Should be ignored
            };

            _mockRepo.Setup(repo => repo.GetAllAsync())
                     .ReturnsAsync(testData);

            // Act
            var result = await _service.GetTop5WeaponsAsync();

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Equal("Strong", result[0].Name); // First should be strongest
            Assert.Equal("Medium", result[1].Name);
            Assert.Equal("Weak", result[2].Name);
        }

        [Fact]
        public async Task GetTop5Weapons_ShouldLimitTo5()
        {
            // Arrange
            var testData = new List<IItem>();
            for (int i = 0; i < 10; i++)
            {
                testData.Add(new Weapon { Name = $"Weapon{i}", Damage = i });
            }

            _mockRepo.Setup(repo => repo.GetAllAsync())
                     .ReturnsAsync(testData);

            // Act
            var result = await _service.GetTop5WeaponsAsync();

            // Assert
            Assert.Equal(5, result.Count);
            Assert.Equal(9, result[0].Damage); // Highest damage first
        }
    }
}
