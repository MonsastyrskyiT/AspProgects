using TaskPriority = PracticAsp11102026.Models.TaskPriority;
using TaskStatus = PracticAsp11102026.Models.TaskStatus;

namespace PracticAsp11102026.DTO
{
    public class TaskItemDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TaskPriority Priority { get; set; }
        public TaskStatus Status { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
