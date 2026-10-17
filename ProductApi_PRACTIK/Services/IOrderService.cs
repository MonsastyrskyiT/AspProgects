using ProductApi.DTO.Order;
using ProductApi.DTO.Pagination;

namespace ProductApi.Services
{
    public interface IOrderService
    {
        Task<PageResponse<OrderDTO>> GetPagedAsync(OrderQueryParams queryParams);
        Task<OrderDTO> CreateAsync(CreateOrderDTO dto);
    }
}