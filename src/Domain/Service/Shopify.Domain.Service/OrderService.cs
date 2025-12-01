using Shopify.Domain.Core.OrderAgg.Data;
using Shopify.Domain.Core.OrderAgg.Dto;
using Shopify.Domain.Core.OrderAgg.Enums;
using Shopify.Domain.Core.OrderAgg.Service;

namespace Shopify.Domain.Service;

public class OrderService (IOrderRepository orderRepository) : IOrderService
{
    public async Task<OrderDto?> GetById(int id, CancellationToken cancellationToken)
    {
        return await orderRepository.GetById(id, cancellationToken);
    }

    public Task<OrderDto?> GetUserActiveOrder(int userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<OrderDto>> GetUserOrders(int userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<OrderDto>> GetAll(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<OrderDto>> GetOrdersByStatus(OrderStatusEnum status, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<OrderItemDto>> GetOrderItems(int orderId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<decimal> GetTotalSales(DateTime from, DateTime to, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}