using UserManagerShop_practic.DTO;
using UserManagerShop_practic.Entities;
using UserManagerShop_practic.Interface;

namespace UserManagerShop.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _products;

    public ProductService(IProductRepository products)
    {
        _products = products;
    }

    public async Task<List<ProductDto>> GetAllAsync()
    {
        var products = await _products.GetAllAsync();
        return products.Select(ToDto).ToList();
    }

    public async Task<ProductDto?> GetByIdAsync(Guid id)
    {
        var product = await _products.GetByIdAsync(id);
        return product is null ? null : ToDto(product);
    }

    public async Task<ProductDto> CreateAsync(CreateProductDto input)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = input.Name,
            Sku = input.Sku,
            Price = input.Price,
            StockQuantity = input.StockQuantity,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        await _products.CreateAsync(product);
        return ToDto(product);
    }

    public Task<bool> DeleteAsync(Guid id) => _products.DeleteAsync(id);

    private static ProductDto ToDto(Product product) => new()
    {
        Id = product.Id,
        Name = product.Name,
        Sku = product.Sku,
        Price = product.Price,
        StockQuantity = product.StockQuantity,
        CreatedAt = product.CreatedAt
    };
}
