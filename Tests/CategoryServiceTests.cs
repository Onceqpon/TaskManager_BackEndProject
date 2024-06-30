using System.Threading.Tasks;
using Xunit;
using Moq;
using Core.Interfaces;
using Application.Services;
using Application.DTOs;
using System.Collections.Generic;
using FluentAssertions;
using Core.Entity;

namespace WebApi.Tests.Services
{
    public class CategoryServiceTests
    {
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly CategoryService _service;

        public CategoryServiceTests()
        {
            _mockCategoryRepository = new Mock<ICategoryRepository>();
            _service = new CategoryService(_mockCategoryRepository.Object);
        }

        [Fact]
        public async Task GetAllCategoriesAsync_ShouldReturnAllCategories()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Category1" },
                new Category { Id = 2, Name = "Category2" }
            };

            _mockCategoryRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(categories);

            // Act
            var result = await _service.GetAllCategoriesAsync();

            // Assert
            result.Should().BeEquivalentTo(categories.Select(c => new CategoryDto { Id = c.Id, Name = c.Name }));
        }

        [Fact]
        public async Task GetCategoryByIdAsync_ShouldReturnCategory_WhenCategoryExists()
        {
            // Arrange
            var category = new Category { Id = 1, Name = "Category1" };

            _mockCategoryRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(category);

            // Act
            var result = await _service.GetCategoryByIdAsync(1);

            // Assert
            result.Should().BeEquivalentTo(new CategoryDto { Id = category.Id, Name = category.Name });
        }

        [Fact]
        public async Task GetCategoryByIdAsync_ShouldReturnNull_WhenCategoryDoesNotExist()
        {
            // Arrange
            _mockCategoryRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Category)null);

            // Act
            var result = await _service.GetCategoryByIdAsync(1);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task AddCategoryAsync_ShouldCallRepository()
        {
            // Arrange
            var createCategoryDto = new CreateCategoryDto { Id = 3, Name = "Category3" };

            // Act
            await _service.AddCategoryAsync(createCategoryDto);

            // Assert
            _mockCategoryRepository.Verify(x => x.AddAsync(It.Is<Category>(c => c.Id == createCategoryDto.Id && c.Name == createCategoryDto.Name)), Times.Once);
        }

        [Fact]
        public async Task UpdateCategoryAsync_ShouldCallRepository_WhenCategoryExists()
        {
            // Arrange
            var updateCategoryDto = new UpdateCategoryDto { Id = 1, Name = "UpdatedCategory" };
            var existingCategory = new Category { Id = 1, Name = "Category1" };

            _mockCategoryRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(existingCategory);

            // Act
            await _service.UpdateCategoryAsync(updateCategoryDto);

            // Assert
            _mockCategoryRepository.Verify(x => x.UpdateAsync(It.Is<Category>(c => c.Id == updateCategoryDto.Id && c.Name == updateCategoryDto.Name)), Times.Once);
        }

        [Fact]
        public async Task DeleteCategoryAsync_ShouldCallRepository_WhenCategoryExists()
        {
            // Arrange
            var category = new Category { Id = 1, Name = "Category1" };

            _mockCategoryRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(category);

            // Act
            await _service.DeleteCategoryAsync(1);

            // Assert
            _mockCategoryRepository.Verify(x => x.DeleteAsync(It.Is<Category>(c => c.Id == category.Id)), Times.Once);
        }

        [Fact]
        public async Task DeleteCategoryAsync_ShouldNotCallRepository_WhenCategoryDoesNotExist()
        {
            // Arrange
            _mockCategoryRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Category)null);

            // Act
            await _service.DeleteCategoryAsync(1);

            // Assert
            _mockCategoryRepository.Verify(x => x.DeleteAsync(It.IsAny<Category>()), Times.Never);
        }
    }
}
