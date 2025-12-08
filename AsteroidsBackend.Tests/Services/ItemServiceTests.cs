using Xunit;
using Moq;
using asteroidsbackend.Services;
using asteroidsbackend.Data;
using asteroidsbackend.Models;
using asteroidsbackend.Models.Interfaces;

namespace AsteroidsBackend.Tests.Services
{
    public class ItemServiceTests
    {
        private readonly Mock<IItemRepository> _mockRepo;
        private readonly ItemService _service;

        public ItemServiceTests()
        {
            _mockRepo = new Mock<IItemRepository>();
            _service = new ItemService(_mockRepo.Object);
        }

        [Fact]
        public async Task AddItem_WithNullItem_ShouldThrowException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.AddItemAsync(null!));
        }

        [Fact]
        public async Task AddItem_WithValidItem_ShouldCallRepository()
        {
            // Arrange
            var item = new Weapon { Name = "Test Laser" };
            _mockRepo.Setup(r => r.AddAsync(It.IsAny<IItem>())).ReturnsAsync(1);

            // Act
            var result = await _service.AddItemAsync(item);

            // Assert
            _mockRepo.Verify(r => r.AddAsync(item), Times.Once);
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task GetAllItems_ShouldReturnItemsFromRepository()
        {
            // Arrange
            var items = new List<IItem> { new Weapon(), new PowerUp() };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(items);

            // Act
            var result = await _service.GetAllItemsAsync();

            // Assert
            Assert.Equal(2, result.Count);
            _mockRepo.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetItemById_ShouldReturnItem_WhenFound()
        {
            // Arrange
            var item = new Weapon { Id = 1, Name = "Test" };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(item);

            // Act
            var result = await _service.GetItemByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test", result.Name);
        }

        [Fact]
        public async Task UpdateItem_WithNullItem_ShouldThrowException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.UpdateItemAsync(null!));
        }

        [Fact]
        public async Task UpdateItem_WithValidItem_ShouldCallRepository()
        {
            // Arrange
            var item = new Weapon { Id = 1, Name = "Updated" };
            _mockRepo.Setup(r => r.UpdateAsync(item)).ReturnsAsync(true);

            // Act
            var result = await _service.UpdateItemAsync(item);

            // Assert
            Assert.True(result);
            _mockRepo.Verify(r => r.UpdateAsync(item), Times.Once);
        }

        [Fact]
        public async Task DeleteItem_ShouldCallRepository()
        {
            // Arrange
            _mockRepo.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _service.DeleteItemAsync(1);

            // Assert
            Assert.True(result);
            _mockRepo.Verify(r => r.DeleteAsync(1), Times.Once);
        }
    }
}
