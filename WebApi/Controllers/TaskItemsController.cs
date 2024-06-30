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
        private readonly ILogger<TaskItemsController> _logger;

        public TaskItemsController(ITaskItemService taskItemService, ILogger<TaskItemsController> logger)
        {
            _taskItemService = taskItemService;
            _logger = logger;
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
        public async Task<IActionResult> CreateTaskItem(CreateTaskItemDto taskItemDto)
        {
            try
            {
                _logger.LogInformation($"Received request to create TaskItem with ProjectId: {taskItemDto.ProjectId} and CategoryId: {taskItemDto.CategoryId}");
                await _taskItemService.AddTaskItemAsync(taskItemDto);
                _logger.LogInformation($"Successfully created TaskItem with Description: {taskItemDto.Description}");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating TaskItem");
                return BadRequest(ex.Message);
            }
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
