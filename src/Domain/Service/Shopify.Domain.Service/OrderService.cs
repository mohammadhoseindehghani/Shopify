using Shopify.Domain.Core.OrderAgg.Data;
using Shopify.Domain.Core.OrderAgg.Dto;
using Shopify.Domain.Core.OrderAgg.Entities;
using Shopify.Domain.Core.OrderAgg.Enums;
using Shopify.Domain.Core.OrderAgg.Service;

namespace Shopify.Domain.Service;

public class OrderService(IOrderRepository orderRepository) : IOrderService
{
    public async Task CreateOrder(int userId, decimal totalAmount, List<OrderItemDto> items, CancellationToken cancellationToken)
    {
        await orderRepository.CreateOrder(userId, totalAmount, items, cancellationToken);
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

    public async Task<OrderDto?> GetUserActiveOrder(int userId, CancellationToken cancellationToken)
    {
        return await orderRepository.GetUserActiveOrder(userId, cancellationToken);
    }

    public async Task<ICollection<OrderDto>> GetUserOrders(int userId, CancellationToken cancellationToken)
    {
        return await orderRepository.GetUserOrders(userId, cancellationToken);
    }

    public async Task<ICollection<OrderDto>> GetAll(CancellationToken cancellationToken)
    {
        return await orderRepository.GetAll(cancellationToken);
    }

    public async Task<ICollection<OrderDto>> GetOrdersByStatus(OrderStatusEnum status, CancellationToken cancellationToken)
    {
        return await orderRepository.GetOrdersByStatus(status, cancellationToken);
    }

    public async Task<ICollection<OrderItemDto>> GetOrderItems(int orderId, CancellationToken cancellationToken)
    {
        return await orderRepository.GetOrderItems(orderId, cancellationToken);
    }


}