using Shopify.Domain.Core.OrderAgg.Data;
using Shopify.Domain.Core.OrderAgg.Dto;
using Shopify.Domain.Core.OrderAgg.Entities;
using Shopify.Domain.Core.OrderAgg.Enums;
using Shopify.Domain.Core.OrderAgg.Service;

namespace Shopify.Domain.Service;

public class OrderService(IOrderRepository orderRepository) : IOrderService
{
    public async Task CreateOrder(int userId, decimal totalAmount, List<OrderItemDto> items, CancellationToken ct)
    {
        var order = new Order
        {
            UserId = userId,
            TotalAmount = totalAmount,
            Status = OrderStatusEnum.Processing,
            IsFinalized = true,
            OrderItems = items.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList()
        };

        await orderRepository.Add(order, ct);
        await orderRepository.SaveChanges(ct);
    }

    public async Task Add(Order order, CancellationToken cancellationToken)
    {
        await orderRepository.Add(order, cancellationToken);
    }

    public async Task SaveChanges(CancellationToken cancellationToken)
    {
        await orderRepository.SaveChanges(cancellationToken);
    }

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