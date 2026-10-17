using Microsoft.AspNetCore.Mvc;
using PracticAsp11102026.DTO;
using PracticAsp11102026.Services;

namespace PracticAsp11102026.Controllers
{
    [ApiController]
    [Route("api/projects")]
    [Produces("application/json")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _projectService.GetAllAsync());
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var project = await _projectService.GetByIdAsync(id);

            if (project == null)
                return NotFound(new { message = $"Project with id: {id} not found" });

            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProjectDTO dto)
        {
            var project = await _projectService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProjectDTO dto)
        {
            var project = await _projectService.UpdateAsync(id, dto);

            if (project == null)
                return NotFound(new { message = $"Project with id: {id} not found" });

            return Ok(project);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _projectService.DeleteAsync(id);

            if (!result)
                return NotFound(new { message = $"Project with id: {id} not found" });

            return NoContent();
        }
    }
}
