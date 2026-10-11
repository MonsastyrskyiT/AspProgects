namespace CRUD.DTO_practic  
{
    public class UpdateOrderDTO_practic
    {
        public string CustomerName { get; set; } = string.Empty;
        public List<CreateOrderItemDTO_practic> Items { get; set; } = new();
    }
}
