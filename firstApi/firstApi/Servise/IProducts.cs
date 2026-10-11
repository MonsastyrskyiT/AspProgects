using firstApi.Models;

namespace firstApi.Servise
{
    public interface IProducts
    {
        List<Product> GetProducts();
        Product? GetById(Guid id);
    }
}
