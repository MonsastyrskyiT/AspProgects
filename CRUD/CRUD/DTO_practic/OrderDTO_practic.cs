namespace CRUD.DTO_practic
{
    public class OrderDTO_practic
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<OrderItemDTO_practic> Items { get; set; } = new();
        public decimal TotalPrice => Items.Sum(i => i.Price * i.Quantity);
    }
}
