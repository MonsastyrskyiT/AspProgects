using Microsoft.EntityFrameworkCore;
using UserManagerShop_practic.Data;
using UserManagerShop_practic.Entities;
using UserManagerShop_practic.Interface;

namespace UserManagerShop_practic.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        public UserRepository(AppDbContext db) => _db = db;

        public Task<List<User>> GetAllAsync() =>
            _db.Users.AsNoTracking().OrderBy(u => u.Email).ToListAsync();

        public Task<User?> GetByIdAsync(Guid id) =>
            _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

        public async Task<User> CreateAsync(User user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user is null) return false;
            user.IsDeleted = true;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
