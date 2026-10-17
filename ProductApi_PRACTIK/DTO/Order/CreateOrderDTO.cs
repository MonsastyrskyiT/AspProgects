namespace ProductApi.DTO.Order
{
    public class CreateOrderDTO
    {
        public string CustomerName { get; set; } = string.Empty;
        public string Status { get; set; } = "New";
        public decimal TotalPrice { get; set; }
    }
}
