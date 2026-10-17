using System.ComponentModel.DataAnnotations;
using TaskPriority = PracticAsp11102026.Models.TaskPriority;
using TaskStatus = PracticAsp11102026.Models.TaskStatus;

namespace PracticAsp11102026.DTO
{
    public class UpdateTaskItemDTO
    {
        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public TaskPriority Priority { get; set; }

        public TaskStatus Status { get; set; }
    }
}
