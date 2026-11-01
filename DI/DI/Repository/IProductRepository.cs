using DI.Entity.Model;

namespace DI.Repository
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> CreateAsync(Product product);
        Task<bool> DeleteAsync(Guid id);

    }
}
