using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RESTWebAPI.Controllers;
using RESTWebAPI.Dtos;
using RESTWebAPI.Models;
using RESTWebAPI.Repositories;

namespace MainControllerTests
{
    public class HomeControllerTests
    {
        private readonly Mock<IItemRepository> repositoryStub = new();

        private readonly Mock<ILogger<HomeController>> loggerStub = new();
        private readonly Random random = new();

        [Fact]
        public async Task GetItemAsync_WithUnexistingItem_ReturnsNotFound()
        {
            // AAA
            // Arrange - Mocks, variables, inputs
            repositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Item)null);

            var controller = new HomeController(repositoryStub.Object, loggerStub.Object);

            // Act
            var result = await controller.GetItemAsync(Guid.NewGuid());

            // Assert
            // Assert.IsType<NotFoundResult>(result.Result);

            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetItemAsync_WithExistingItem_ReturnsExpectedItem()
        {
            // Arrange
            var expectedItem = CreateRandomItem();

            repositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<Guid>()))
                .ReturnsAsync(expectedItem);

            var controller = new HomeController(repositoryStub.Object, loggerStub.Object);
            // Act
            var result = await controller.GetItemAsync(Guid.NewGuid());
            // Assert
            result.Value.Should().BeEquivalentTo(
                expectedItem,
                options => options.ComparingByMembers<Item>());
        }

        [Fact]
        public async Task GetItemsAsync_WithExistingItems_ReturnsAllItems()
        {
            // Arrange
            var expectedItems = new[] { CreateRandomItem(), CreateRandomItem(), CreateRandomItem() };

            repositoryStub.Setup(repo => repo.GetItemsAsync())
                .ReturnsAsync(expectedItems);

            var controller = new HomeController(repositoryStub.Object, loggerStub.Object);
            // Act
            var result = await controller.GetItemsAsync();
            // Assert
            result.Should().BeEquivalentTo(
                expectedItems,
                options => options.ComparingByMembers<Item>());
        }

        [Fact]

        public async Task CreateItemAsync_WithItemToCreate_ReturnsCreatedItem()
        {
            // Arrange
            var itemToCreate = new CreateItemDto()
            {
                Name = Guid.NewGuid().ToString(),
                Price = random.Next(100)
            };

            var controller = new HomeController(repositoryStub.Object, loggerStub.Object);
            // Act
            var result = await controller.CreateItemAsync(itemToCreate);
            // Assert
            var createdItem = (result.Result as CreatedAtActionResult).Value as ItemDto;
            itemToCreate.Should().BeEquivalentTo(
                createdItem,
                options => options.ComparingByMembers<ItemDto>().ExcludingMissingMembers());

            createdItem.Id.Should().NotBeEmpty();   
            createdItem.CreatedDate.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1000));
        }

        private Item CreateRandomItem()
        {
            return new Item
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                Price = random.Next(100),
                CreatedDate = DateTimeOffset.UtcNow
            };
        }
    }
}
