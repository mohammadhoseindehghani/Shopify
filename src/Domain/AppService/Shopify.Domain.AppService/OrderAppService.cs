using Shopify.Domain.Core._common;
using Shopify.Domain.Core.CartAgg.Service;
using Shopify.Domain.Core.OrderAgg.AppService;
using Shopify.Domain.Core.OrderAgg.Dto;
using Shopify.Domain.Core.OrderAgg.Enums;
using Shopify.Domain.Core.OrderAgg.Service;
using Shopify.Domain.Core.ProductAgg.Service;
using Shopify.Domain.Core.UserAgg.Service;

namespace Shopify.Domain.AppService;

public class OrderAppService(IOrderService orderService,IUserService userService, ICartService cartService,IProductService productService) : IOrderAppService
{
    public async Task<Result<bool>> FinalizeOrder(int userId, CancellationToken cancellationToken)
    {
     
        var cartDto = await cartService.GetUserCart(userId, cancellationToken);
        if (cartDto == null || !cartDto.Items.Any())
            return Result<bool>.Failure("سبد خرید شما خالی است.");

   
        var user = await userService.GetById(userId, cancellationToken);
        if (user == null)
            return Result<bool>.Failure("کاربر یافت نشد.");

        decimal totalAmount = 0;
        var orderItemsToCreate = new List<OrderItemDto>();

      
        foreach (var item in cartDto.Items)
        {
            var product = await productService.GetById(item.ProductId, cancellationToken);

            if (product == null)
                return Result<bool>.Failure($"محصول با شناسه {item.ProductId} یافت نشد.");

            if (product.StockQuantity < item.Quantity)
                return Result<bool>.Failure($"موجودی محصول {product.Title} کافی نیست.");

        
            totalAmount += product.Price * item.Quantity;

        
            orderItemsToCreate.Add(new OrderItemDto
            {
                ProductId = product.Id,
                Quantity = item.Quantity,
                UnitPrice = product.Price
            });
        }

       
        if (user.Balance < totalAmount)
        {
            return Result<bool>.Failure("موجودی کیف پول شما کافی نیست.");
        }

        try
        {
            foreach (var item in cartDto.Items)
            {
                await productService.ReduceStock(item.ProductId, item.Quantity, cancellationToken);
            }

            await userService.DeductBalance(userId, totalAmount, cancellationToken);

            await orderService.CreateOrder(userId, totalAmount, orderItemsToCreate, cancellationToken);

            await cartService.ClearCart(cartDto.Id, cancellationToken);

            return Result<bool>.Success();
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"خطا در ثبت سفارش: {ex.Message}");
        }
    }
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