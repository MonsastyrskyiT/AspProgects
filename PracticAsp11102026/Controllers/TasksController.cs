using Microsoft.AspNetCore.Mvc;
using PracticAsp11102026.DTO;
using PracticAsp11102026.Services;
using TaskStatus = PracticAsp11102026.Models.TaskStatus;

namespace PracticAsp11102026.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    [Produces("application/json")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskItemService _taskService;

        public TasksController(ITaskItemService taskService)
        {
            _taskService = taskService;
        }

        // GET api/tasks?projectId=...&status=InProgress
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Guid? projectId, [FromQuery] TaskStatus? status)
        {
            return Ok(await _taskService.GetAllAsync(projectId, status));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var task = await _taskService.GetByIdAsync(id);

            if (task == null)
                return NotFound(new { message = $"Task with id: {id} not found" });

            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskItemDTO dto)
        {
            var task = await _taskService.CreateAsync(dto);

            if (task == null)
                return NotFound(new { message = $"Project with id: {dto.ProjectId} not found" });

            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaskItemDTO dto)
        {
            var task = await _taskService.UpdateAsync(id, dto);

            if (task == null)
                return NotFound(new { message = $"Task with id: {id} not found" });

            return Ok(task);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _taskService.DeleteAsync(id);

            if (!result)
                return NotFound(new { message = $"Task with id: {id} not found" });

            return NoContent();
        }
    }
}
