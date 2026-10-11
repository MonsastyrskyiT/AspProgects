using SvaggerOpenIP.DTO;
using SvaggerOpenIP.DTO.SwaggerOpenApi.DTO;

namespace SvaggerOpenIP.Services
{
    public interface IProductService
    {
        ProductDTO Create(CreateProductDTO product);
        public List<ProductDTO> GetProducts();
    }
}
