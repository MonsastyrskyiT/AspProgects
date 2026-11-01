using UserManagerShop_practic.DTO;
using UserManagerShop_practic.Entities;
using UserManagerShop_practic.Interface;

namespace UserManagerShop_practic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _users;

        public UserService(IUserRepository users)
        {
            _users = users;
        }

        public async Task<List<UserDto>> GetUsersAsync()
        {
            var users = await _users.GetAllAsync();
            return users.Select(ToDto).ToList();
        }

        public async Task<UserDto?> GetUserByIdAsync(Guid id)
        {
            var user = await _users.GetByIdAsync(id);
            return user is null ? null : ToDto(user);
        }

        public async Task<UserDto> CreateAsync(CreateUserDto input)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = input.Email,
                FirstName = input.FirstName,
                LastName = input.LastName,
                Role = input.Role,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };
            await _users.CreateAsync(user);
            return ToDto(user);
        }

        public Task<bool> DeleteAsync(Guid id) => _users.DeleteAsync(id);

        private static UserDto ToDto(User user) => new()
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Role = user.Role,
            CreatedAt = user.CreatedAt
        };
    }
}
