using CRUD.DTO;
namespace CRUD.Services
{

    public class ProductSrvice : IProductService
    {
        private static readonly List<ProductsDTO> products = new()
{
    new ProductsDTO { Id = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), Name = "Laptop", Price = 1000 },
    new ProductsDTO { Id = Guid.Parse("b2c3d4e5-f6a7-8901-bcde-f12345678901"), Name = "Computer", Price = 1500 }
};

        public ProductsDTO Create(CreateProductDTO product)
        {
            var newProduct = new ProductsDTO
            {
                Id = Guid.NewGuid(),
                Name = product.Name,
                Price = product.Price
            };
            products.Add(newProduct);
            return newProduct;
        }

        public void Delete(Guid id)
        {
            var product = GetById(id);
            products.Remove(product);
        }

        public List<ProductsDTO> GetAll()
        {
            return products;
        }

        public ProductsDTO GetById(Guid id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                throw new Exception($"Product not found by {id}");
            }
            return product;

        }

        public void Update(Guid id, UpdatePriductDTO product)
        {
            var updatedProduct = GetById(id);
            updatedProduct.Name = product.Name;
            updatedProduct.Price = product.Price;
        }
    }
}
