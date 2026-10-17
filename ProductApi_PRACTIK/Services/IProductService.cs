using ProductApi.Data.Models;
using ProductApi.DTO.Pagination;
using ProductApi.DTO.Product;

namespace ProductApi.Services
{
    public interface IProductService
    {
        //Task<List<ProductDTO>> GetAllProductsAsync();
        //Task<PageResponse<ProductDTO>> GetAllProductsAsync(int pageNumber, int pageSize);
        Task<ProductDTO?> GetProductByIdAsync(Guid id);
        Task<ProductDTO> CreateProductAsync(CreateProductDTO product);
        Task<bool> DeleteAsync(Guid id);
        Task<PageResponse<ProductDTO>> GetPagedAsync(ProductQueryParams queryParams);
    }
}
