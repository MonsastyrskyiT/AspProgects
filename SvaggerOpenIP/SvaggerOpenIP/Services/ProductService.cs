using SvaggerOpenIP.DTO;
using SvaggerOpenIP.DTO.SwaggerOpenApi.DTO;
using AutoMapper;
using SvaggerOpenIP.DataBase;

namespace SvaggerOpenIP.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        //    private static readonly List<ProductDTO> products = new List<ProductDTO>
        //{
        //    new ProductDTO { Name = "Laptop", Price = 1000 },
        //    new ProductDTO { Name = "Computer", Price = 1500 }
        //};
        private static readonly List<Product> _database = new();
        public ProductService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public ProductDTO Create(CreateProductDTO product)
        {
            var newProduct = _mapper.Map<Product>(product);
            _database.Add(newProduct);
            return _mapper.Map<ProductDTO>(newProduct);
        }
        public List<ProductDTO> GetProducts()
        {
            var productList = _mapper.Map<List<ProductDTO>>(_database);
            return _mapper.Map<List<ProductDTO>>(_database);
        }
    }
}