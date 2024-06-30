using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITaskItemService
    {
        Task<TaskItemDto> GetTaskItemByIdAsync(int id);
        Task<IEnumerable<TaskItemDto>> GetAllTaskItemsAsync();
        Task AddTaskItemAsync(CreateTaskItemDto taskItemDto);
        Task UpdateTaskItemAsync(UpdateTaskItemDto taskItemDto);
        Task DeleteTaskItemAsync(int id);
    }
}
