using System.ComponentModel.DataAnnotations;

namespace FileAvatar.DTO
{
    public class CreateUserDTO
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
        public int Age { get; set; }
        public IFormFile? Avatar { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Avatar != null)
            {
                var allowedExtentions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                var extentions = Path.GetExtension(Avatar.FileName).ToLowerInvariant();
                if (!allowedExtentions.Contains(extentions))
                {
                    yield return new ValidationResult(
                        "Invalid extantion", new[] { nameof(Avatar) }
                    );
                }

                const long maxFileSize = 2 * 1024 * 1024;
                if (Avatar.Length > maxFileSize)
                {
                    yield return new ValidationResult("Size of avatar must be less then 2 MB",
                        new[] { nameof(Avatar) });
                }
            }
        }
    }
}
