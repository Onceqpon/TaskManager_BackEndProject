using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Services;
using Core.Entity;
using Core.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace WebApi.Tests.Services
{
    public class TaskItemServiceTests
    {
        private readonly Mock<ITaskItemRepository> _mockTaskItemRepository;
        private readonly Mock<IProjectRepository> _mockProjectRepository;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly Mock<ILogger<TaskItemService>> _mockLogger;
        private readonly TaskItemService _service;

        public TaskItemServiceTests()
        {
            _mockTaskItemRepository = new Mock<ITaskItemRepository>();
            _mockProjectRepository = new Mock<IProjectRepository>();
            _mockCategoryRepository = new Mock<ICategoryRepository>();
            _mockLogger = new Mock<ILogger<TaskItemService>>();

            _service = new TaskItemService(
                _mockTaskItemRepository.Object,
                _mockProjectRepository.Object,
                _mockCategoryRepository.Object,
                _mockLogger.Object);
        }

        [Fact]
        public async Task AddTaskItemAsync_ShouldThrowException_WhenProjectDoesNotExist()
        {
            // Arrange
            var createTaskItemDto = new CreateTaskItemDto { Description = "Task1", ProjectId = 1, CategoryId = 1 };
            _mockProjectRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Project)null);

            // Act
            Func<Task> act = async () => await _service.AddTaskItemAsync(createTaskItemDto);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Invalid ProjectId");
        }

        [Fact]
        public async Task AddTaskItemAsync_ShouldThrowException_WhenCategoryDoesNotExist()
        {
            // Arrange
            var createTaskItemDto = new CreateTaskItemDto { Description = "Task1", ProjectId = 1, CategoryId = 1 };
            var project = new Project { Id = 1, Name = "Project1" };
            _mockProjectRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(project);
            _mockCategoryRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Category)null);

            // Act
            Func<Task> act = async () => await _service.AddTaskItemAsync(createTaskItemDto);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Invalid CategoryId");
        }

        [Fact]
        public async Task AddTaskItemAsync_ShouldCallRepository_WhenProjectAndCategoryExist()
        {
            // Arrange
            var createTaskItemDto = new CreateTaskItemDto { Description = "Task1", ProjectId = 1, CategoryId = 1 };
            var project = new Project { Id = 1, Name = "Project1" };
            var category = new Category { Id = 1, Name = "Category1" };

            _mockProjectRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(project);
            _mockCategoryRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(category);

            // Act
            await _service.AddTaskItemAsync(createTaskItemDto);

            // Assert
            _mockTaskItemRepository.Verify(x => x.AddAsync(It.Is<TaskItem>(t => t.Description == createTaskItemDto.Description && t.ProjectId == createTaskItemDto.ProjectId && t.CategoryId == createTaskItemDto.CategoryId)), Times.Once);
        }

        [Fact]
        public async Task UpdateTaskItemAsync_ShouldThrowException_WhenProjectDoesNotExist()
        {
            // Arrange
            var updateTaskItemDto = new UpdateTaskItemDto { Id = 1, Description = "Task1", ProjectId = 1, CategoryId = 1 };
            _mockTaskItemRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new TaskItem());
            _mockProjectRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Project)null);

            // Act
            Func<Task> act = async () => await _service.UpdateTaskItemAsync(updateTaskItemDto);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Invalid ProjectId");
        }

        [Fact]
        public async Task UpdateTaskItemAsync_ShouldThrowException_WhenCategoryDoesNotExist()
        {
            // Arrange
            var updateTaskItemDto = new UpdateTaskItemDto { Id = 1, Description = "Task1", ProjectId = 1, CategoryId = 1 };
            var taskItem = new TaskItem { Id = 1, Description = "Task1", ProjectId = 1, CategoryId = 1 };
            var project = new Project { Id = 1, Name = "Project1" };

            _mockTaskItemRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(taskItem);
            _mockProjectRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(project);
            _mockCategoryRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Category)null);

            // Act
            Func<Task> act = async () => await _service.UpdateTaskItemAsync(updateTaskItemDto);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Invalid CategoryId");
        }

        [Fact]
        public async Task UpdateTaskItemAsync_ShouldCallRepository_WhenProjectAndCategoryExist()
        {
            // Arrange
            var updateTaskItemDto = new UpdateTaskItemDto { Id = 1, Description = "Task1", ProjectId = 1, CategoryId = 1 };
            var taskItem = new TaskItem { Id = 1, Description = "Task1", ProjectId = 1, CategoryId = 1 };
            var project = new Project { Id = 1, Name = "Project1" };
            var category = new Category { Id = 1, Name = "Category1" };

            _mockTaskItemRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(taskItem);
            _mockProjectRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(project);
            _mockCategoryRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(category);

            // Act
            await _service.UpdateTaskItemAsync(updateTaskItemDto);

            // Assert
            _mockTaskItemRepository.Verify(x => x.UpdateAsync(It.Is<TaskItem>(t => t.Description == updateTaskItemDto.Description && t.ProjectId == updateTaskItemDto.ProjectId && t.CategoryId == updateTaskItemDto.CategoryId)), Times.Once);
        }

        [Fact]
        public async Task DeleteTaskItemAsync_ShouldCallRepository_WhenTaskItemExists()
        {
            // Arrange
            var taskItem = new TaskItem { Id = 1, Description = "Task1", ProjectId = 1, CategoryId = 1 };

            _mockTaskItemRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(taskItem);

            // Act
            await _service.DeleteTaskItemAsync(1);

            // Assert
            _mockTaskItemRepository.Verify(x => x.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task DeleteTaskItemAsync_ShouldNotCallRepository_WhenTaskItemDoesNotExist()
        {
            // Arrange
            _mockTaskItemRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((TaskItem)null);

            // Act
            await _service.DeleteTaskItemAsync(1);

            // Assert
            _mockTaskItemRepository.Verify(x => x.DeleteAsync(It.IsAny<int>()), Times.Never);
        }
    }
}
