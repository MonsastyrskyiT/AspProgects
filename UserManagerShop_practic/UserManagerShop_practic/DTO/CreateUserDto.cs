using System.ComponentModel.DataAnnotations;

namespace UserManagerShop_practic.DTO
{
    public class CreateUserDto
    {
        [Required, EmailAddress, MaxLength(256)]
        public string Email { get; set; } = string.Empty;
        [Required, MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;
        [Required, MaxLength(100)]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = string.Empty;
    }
}
