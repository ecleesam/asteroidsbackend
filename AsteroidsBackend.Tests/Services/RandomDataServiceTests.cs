using Xunit;
using Moq;
using asteroidsbackend.Services;
using asteroidsbackend.Data;
using asteroidsbackend.Models;
using asteroidsbackend.Models.Interfaces;

namespace AsteroidsBackend.Tests.Services
{
    public class RandomDataServiceTests
    {
        private readonly Mock<IItemRepository> _mockRepo;
        private readonly RandomDataService _service;

        public RandomDataServiceTests()
        {
            _mockRepo = new Mock<IItemRepository>();
            _service = new RandomDataService(_mockRepo.Object);
        }

        [Fact]
        public async Task GenerateSampleDataset_ShouldClearAndAddItems()
        {
            // Arrange
            _mockRepo.Setup(r => r.ClearAsync()).Returns(Task.CompletedTask);
            _mockRepo.Setup(r => r.AddAsync(It.IsAny<IItem>())).ReturnsAsync(1);

            // Act
            await _service.GenerateSampleDatasetAsync();

            // Assert
            _mockRepo.Verify(r => r.ClearAsync(), Times.Once);
            // 10 weapons + 5 powerups = 15 items
            _mockRepo.Verify(r => r.AddAsync(It.IsAny<IItem>()), Times.Exactly(15));
            _mockRepo.Verify(r => r.AddAsync(It.Is<IItem>(i => i.Category == Category.Weapon)), Times.Exactly(10));
            _mockRepo.Verify(r => r.AddAsync(It.Is<IItem>(i => i.Category == Category.PowerUp)), Times.Exactly(5));
        }
    }
}
