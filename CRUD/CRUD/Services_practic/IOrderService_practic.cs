using CRUD.DTO;
using CRUD.DTO_practic;

namespace CRUD.Services_practic
{
    public interface IOrderService_practic
    {
        List<OrderDTO_practic> GetAll();
        OrderDTO_practic GetById(Guid id);//
        OrderDTO_practic Create(CreateOrderDTO_practic order);
        void Update(Guid id, UpdateOrderDTO_practic order);
        void Delete(Guid id);
    }
}
