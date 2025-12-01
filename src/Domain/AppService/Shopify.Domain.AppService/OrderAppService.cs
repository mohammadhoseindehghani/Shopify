using Shopify.Domain.Core._common;
using Shopify.Domain.Core.OrderAgg.AppService;
using Shopify.Domain.Core.OrderAgg.Dto;
using Shopify.Domain.Core.OrderAgg.Enums;
using Shopify.Domain.Core.OrderAgg.Service;

namespace Shopify.Domain.AppService;

public class OrderAppService(IOrderService orderService) : IOrderAppService
{
    public async Task<Result<OrderDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var order =  await orderService.GetById(id, cancellationToken);
        if (order == null)
            return Result<OrderDto>.Failure("سفارش یافت نشد");
        return Result<OrderDto>.Success(order);
    }

    public async Task<Result<OrderDto>> GetUserActiveOrder(int userId, CancellationToken cancellationToken)
    {
        var order = await orderService.GetUserActiveOrder(userId, cancellationToken);
        if (order == null)
            return Result<OrderDto>.Failure("هیچ سفارس فعالی برای این یوزر یافت نشد");
        return Result<OrderDto>.Success(order);
    }

    public async Task<ICollection<OrderDto>> GetUserOrders(int userId, CancellationToken cancellationToken)
    {
        return await orderService.GetUserOrders(userId, cancellationToken);
    }

    public async Task<ICollection<OrderDto>> GetAll(CancellationToken cancellationToken)
    {
        return await orderService.GetAll(cancellationToken);
    }

    public async Task<ICollection<OrderDto>> GetOrdersByStatus(OrderStatusEnum status, CancellationToken cancellationToken)
    {
        return await orderService.GetOrdersByStatus(status, cancellationToken);
    }

    public async Task<ICollection<OrderItemDto>> GetOrderItems(int orderId, CancellationToken cancellationToken)
    {
        return await orderService.GetOrderItems(orderId, cancellationToken);
    }
}