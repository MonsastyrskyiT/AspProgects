using UserManagerShop_practic.DTO;

namespace UserManagerShop_practic.Interface
{
    public interface IUserService
    {
        Task<List<UserDto>> GetUsersAsync();
        Task<UserDto?> GetUserByIdAsync(Guid id);
        Task<UserDto> CreateAsync(CreateUserDto user);
        Task<bool> DeleteAsync(Guid id);
    }
}
