using CRUD.DTO;

namespace CRUD.Services
{
    public interface IProductService
    {
        List<ProductsDTO> GetAll();
        ProductsDTO GetById(Guid id);
        ProductsDTO Create(CreateProductDTO product);
        void Update(Guid id, UpdatePriductDTO product);
        void Delete(Guid id);
    }
}
