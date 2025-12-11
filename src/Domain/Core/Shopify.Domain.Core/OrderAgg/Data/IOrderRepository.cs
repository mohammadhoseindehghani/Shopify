using Shopify.Domain.Core.OrderAgg.Dto;
using Shopify.Domain.Core.OrderAgg.Entities;
using Shopify.Domain.Core.OrderAgg.Enums;

namespace Shopify.Domain.Core.OrderAgg.Data;

public interface IOrderRepository
{
    Task Add(Order order, CancellationToken cancellationToken);
    Task CreateOrder(int userId, decimal totalAmount, List<OrderItemDto> items, CancellationToken cancellationToken);
    Task SaveChanges(CancellationToken cancellationToken);
    Task<OrderDto?> GetById(int id, CancellationToken cancellationToken);
    Task<OrderDto?> GetUserActiveOrder(int userId, CancellationToken cancellationToken);
    Task<ICollection<OrderDto>> GetUserOrders(int userId, CancellationToken cancellationToken);
    Task<ICollection<OrderDto>> GetAll(CancellationToken cancellationToken);
    Task<ICollection<OrderDto>> GetOrdersByStatus(OrderStatusEnum status, CancellationToken cancellationToken);
    Task<ICollection<OrderItemDto>> GetOrderItems(int orderId, CancellationToken cancellationToken);
    Task<int> OrderCount(CancellationToken cancellationToken);
    Task<decimal> TotalOrder(CancellationToken cancellationToken);
}
