using Microsoft.EntityFrameworkCore;
using UserManagerShop_practic.Data;
using UserManagerShop_practic.Entities;
using UserManagerShop_practic.Interface;

namespace UserManagerShop_practic.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _db;
        public ProductRepository(AppDbContext db) => _db = db;

        public Task<List<Product>> GetAllAsync() =>
            _db.Products.AsNoTracking()
                .OrderByDescending(p => p.CreatedAt).ToListAsync();

        public Task<Product?> GetByIdAsync(Guid id) =>
            _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

        public async Task<Product> CreateAsync(Product product)
        {
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product is null) return false;
            product.IsDeleted = true;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
