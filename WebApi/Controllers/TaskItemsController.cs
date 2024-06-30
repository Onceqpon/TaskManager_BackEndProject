using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemsController : ControllerBase
    {
        private readonly ITaskItemService _taskItemService;

        public TaskItemsController(ITaskItemService taskItemService)
        {
            _taskItemService = taskItemService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskItemById(int id)
        {
            var taskItem = await _taskItemService.GetTaskItemByIdAsync(id);
            if (taskItem == null)
            {
                return NotFound();
            }
            return Ok(taskItem);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTaskItems()
        {
            var taskItems = await _taskItemService.GetAllTaskItemsAsync();
            return Ok(taskItems);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskItem([FromBody] CreateTaskItemDto taskItemDto)
        {
            if (taskItemDto == null)
            {
                return BadRequest();
            }
            await _taskItemService.AddTaskItemAsync(taskItemDto);
            return CreatedAtAction(nameof(GetTaskItemById), new { id = taskItemDto.Id }, taskItemDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaskItem(int id, [FromBody] UpdateTaskItemDto taskItemDto)
        {
            if (taskItemDto == null || id != taskItemDto.Id)
            {
                return BadRequest();
            }
            await _taskItemService.UpdateTaskItemAsync(taskItemDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskItem(int id)
        {
            await _taskItemService.DeleteTaskItemAsync(id);
            return NoContent();
        }
    }
}
