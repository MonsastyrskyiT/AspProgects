using UserManagerShop_practic.Entities;

namespace UserManagerShop_practic.Interface
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid id);
        Task<User> CreateAsync(User user);
        Task<bool> DeleteAsync(Guid id);
    }
}
