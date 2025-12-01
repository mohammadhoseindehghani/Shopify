using Shopify.Domain.Core.OrderAgg.Dto;
using Shopify.Domain.Core.OrderAgg.Enums;

namespace Shopify.Domain.Core.OrderAgg.Service;

public interface IOrderService
{
    Task<OrderDto?> GetById(int id, CancellationToken cancellationToken);
    Task<OrderDto?> GetUserActiveOrder(int userId, CancellationToken cancellationToken);
    Task<ICollection<OrderDto>> GetUserOrders(int userId, CancellationToken cancellationToken);
    Task<ICollection<OrderDto>> GetAll(CancellationToken cancellationToken);
    Task<ICollection<OrderDto>> GetOrdersByStatus(OrderStatusEnum status, CancellationToken cancellationToken);
    Task<ICollection<OrderItemDto>> GetOrderItems(int orderId, CancellationToken cancellationToken);
}