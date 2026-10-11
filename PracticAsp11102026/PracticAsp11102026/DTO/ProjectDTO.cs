namespace PracticAsp11102026.DTO
{
    public class ProjectDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<TaskItemDTO> Tasks { get; set; } = new();
    }
}
