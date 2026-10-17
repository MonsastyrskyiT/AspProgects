using firstApi.Models;

namespace firstApi.Servise
{
    public class Productservise : IProducts
    {
        

        private readonly List<Product> _products = new List<Product>
    {
        new Product {Id = new Guid(), Name = "Laptop", Price = 200000},
        new Product {Id = new Guid(), Name = "Mouse", Price = 1500}
    };

        public Product? GetById(Guid id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public List<Product> GetProducts()
        {
            return _products;
        }
    }
}

