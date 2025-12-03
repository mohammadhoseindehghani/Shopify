using Shopify.Domain.Core._common;
using Shopify.Domain.Core.OrderAgg.Dto;
using Shopify.Domain.Core.OrderAgg.Enums;

namespace Shopify.Domain.Core.OrderAgg.AppService;

public interface IOrderAppService
{
    Task<Result<bool>> FinalizeOrder(int userId, CancellationToken cancellationToken);
    Task<Result<OrderDto>> GetById(int id, CancellationToken cancellationToken);
    Task<Result<OrderDto>> GetUserActiveOrder(int userId, CancellationToken cancellationToken);
    Task<ICollection<OrderDto>> GetUserOrders(int userId, CancellationToken cancellationToken);
    Task<ICollection<OrderDto>> GetAll(CancellationToken cancellationToken);
    Task<ICollection<OrderDto>> GetOrdersByStatus(OrderStatusEnum status, CancellationToken cancellationToken);
    Task<ICollection<OrderItemDto>> GetOrderItems(int orderId, CancellationToken cancellationToken);
}