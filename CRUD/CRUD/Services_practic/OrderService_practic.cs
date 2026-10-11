using CRUD.DTO;
using CRUD.DTO_practic;
using CRUD.Services;

namespace CRUD.Services_practic
{
    public class OrderService_practic : IOrderService_practic
    {
        private readonly IProductService _productService;
        private static readonly List<OrderDTO_practic> orders = new();

        public OrderService_practic(IProductService productService)
        {
            _productService = productService;
        }

        public OrderDTO_practic Create(CreateOrderDTO_practic order)
        {
            var items = new List<OrderItemDTO_practic>();

            foreach (var item in order.Items)
            {
                var product = _productService.GetById(item.ProductId);

                items.Add(new OrderItemDTO_practic
                {
                    Id = Guid.NewGuid(),
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Quantity = item.Quantity,
                    Price = product.Price
                });
            }

            var newOrder = new OrderDTO_practic
            {
                Id = Guid.NewGuid(),
                CustomerName = order.CustomerName,
                CreatedAt = DateTime.UtcNow,
                Items = items
            };

            orders.Add(newOrder);
            return newOrder;
        }

        public void Delete(Guid id)
        {
            var order = GetById(id);
            orders.Remove(order);
        }

        public List<OrderDTO_practic> GetAll()
        {
            return orders;
        }

        public OrderDTO_practic GetById(Guid id)
        {
            var order = orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                throw new Exception($"Order not found by {id}");
            }
            return order;
        }

        public void Update(Guid id, UpdateOrderDTO_practic order)
        {
            var existingOrder = GetById(id);
            existingOrder.CustomerName = order.CustomerName;

            var items = new List<OrderItemDTO_practic>();
            foreach (var item in order.Items)
            {
                var product = _productService.GetById(item.ProductId);

                items.Add(new OrderItemDTO_practic
                {
                    Id = Guid.NewGuid(),
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Quantity = item.Quantity,
                    Price = product.Price
                });
            }

            existingOrder.Items = items;
        }
    }
}
