using Application.DTOs;
using Application.Interfaces;
using Core.Entity;
using Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<TaskItemService> _logger;

        public TaskItemService(
            ITaskItemRepository taskItemRepository,
            IProjectRepository projectRepository,
            ICategoryRepository categoryRepository,
            ILogger<TaskItemService> logger)
        {
            _taskItemRepository = taskItemRepository;
            _projectRepository = projectRepository;
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<TaskItemDto> GetTaskItemByIdAsync(int id)
        {
            var taskItem = await _taskItemRepository.GetByIdAsync(id);
            return new TaskItemDto
            {
                Id = taskItem.Id,
                Description = taskItem.Description,
                CategoryId = taskItem.CategoryId,
                ProjectId = taskItem.ProjectId,
                IsCompleted = taskItem.IsCompleted
            };
        }

        public async Task<IEnumerable<TaskItemDto>> GetAllTaskItemsAsync()
        {
            var taskItems = await _taskItemRepository.GetAllAsync();
            return taskItems.Select(t => new TaskItemDto
            {
                Id = t.Id,
                Description = t.Description,
                CategoryId = t.CategoryId,
                ProjectId = t.ProjectId,
                IsCompleted = t.IsCompleted
            });
        }

        public async Task AddTaskItemAsync(CreateTaskItemDto taskItemDto)
        {
            _logger.LogInformation($"Attempting to add TaskItem with ProjectId: {taskItemDto.ProjectId} and CategoryId: {taskItemDto.CategoryId}");

            var project = await _projectRepository.GetByIdAsync(taskItemDto.ProjectId);
            if (project == null)
            {
                _logger.LogError($"Project with Id {taskItemDto.ProjectId} not found.");
                throw new Exception("Invalid ProjectId");
            }

            var category = await _categoryRepository.GetByIdAsync(taskItemDto.CategoryId);
            if (category == null)
            {
                _logger.LogError($"Category with Id {taskItemDto.CategoryId} not found.");
                throw new Exception("Invalid CategoryId");
            }

            var taskItem = new TaskItem
            {
                Description = taskItemDto.Description,
                CategoryId = taskItemDto.CategoryId,
                ProjectId = taskItemDto.ProjectId,
                IsCompleted = false
            };

            _logger.LogInformation($"Adding TaskItem: {taskItem.Description}, ProjectId: {taskItem.ProjectId}, CategoryId: {taskItem.CategoryId}");
            await _taskItemRepository.AddAsync(taskItem);
        }

        public async Task UpdateTaskItemAsync(UpdateTaskItemDto taskItemDto)
        {
            _logger.LogInformation($"Updating TaskItem with Id: {taskItemDto.Id}, ProjectId: {taskItemDto.ProjectId}, CategoryId: {taskItemDto.CategoryId}");

            var taskItem = await _taskItemRepository.GetByIdAsync(taskItemDto.Id);
            if (taskItem != null)
            {
                var project = await _projectRepository.GetByIdAsync(taskItemDto.ProjectId);
                var category = await _categoryRepository.GetByIdAsync(taskItemDto.CategoryId);

                if (project == null)
                {
                    _logger.LogError($"Invalid ProjectId: {taskItemDto.ProjectId}");
                    throw new Exception("Invalid ProjectId");
                }

                if (category == null)
                {
                    _logger.LogError($"Invalid CategoryId: {taskItemDto.CategoryId}");
                    throw new Exception("Invalid CategoryId");
                }

                taskItem.Description = taskItemDto.Description;
                taskItem.CategoryId = taskItemDto.CategoryId;
                taskItem.ProjectId = taskItemDto.ProjectId;
                taskItem.IsCompleted = taskItemDto.IsCompleted;
                await _taskItemRepository.UpdateAsync(taskItem);
            }
        }

        public async Task DeleteTaskItemAsync(int id)
        {
            var taskItem = await _taskItemRepository.GetByIdAsync(id);
            if (taskItem != null)
            {
                await _taskItemRepository.DeleteAsync(id);
            }
        }
    }
}
