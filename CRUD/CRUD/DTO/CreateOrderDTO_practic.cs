using CRUD.DTO_practic;

namespace CRUD.DTO
{
    public class CreateOrderDTO_practic
    {
        public string CustomerName { get; set; } = string.Empty;
        public List<CreateOrderItemDTO_practic> Items { get; set; } = new();
    }
}
