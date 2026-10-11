using System.ComponentModel.DataAnnotations;

namespace PracticAsp11102026.DTO
{
    public class CreateProjectDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
