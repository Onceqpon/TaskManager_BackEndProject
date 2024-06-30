using Application.DTOs;
using Application.Interfaces;
using Core.Entity;
using Core.Interfaces;


namespace Application.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly ITaskItemRepository _taskItemRepository;

        public TaskItemService(ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
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
            var taskItem = new TaskItem
            {
                Id = taskItemDto.Id,
                Description = taskItemDto.Description,
                CategoryId = taskItemDto.CategoryId,
                ProjectId = taskItemDto.ProjectId
            };
            await _taskItemRepository.AddAsync(taskItem);
        }

        public async Task UpdateTaskItemAsync(UpdateTaskItemDto taskItemDto)
        {
            var taskItem = await _taskItemRepository.GetByIdAsync(taskItemDto.Id);
            if (taskItem != null)
            {
                taskItem.Description = taskItemDto.Description;
                taskItem.CategoryId = taskItemDto.CategoryId;
                taskItem.ProjectId = taskItemDto.ProjectId;
                taskItem.IsCompleted = taskItemDto.IsCompleted;
                await _taskItemRepository.UpdateAsync(taskItem);
            }
        }

        public async Task DeleteTaskItemAsync(int id)
        {
            await _taskItemRepository.DeleteAsync(id);
        }
    }
}