using UserManagerShop_practic.DTO;

namespace UserManagerShop_practic.Interface
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllAsync();
        Task<ProductDto?> GetByIdAsync(Guid id);
        Task<ProductDto> CreateAsync(CreateProductDto product);
        Task<bool> DeleteAsync(Guid id);
    }
}

