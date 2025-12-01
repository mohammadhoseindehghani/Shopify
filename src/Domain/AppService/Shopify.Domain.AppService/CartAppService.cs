using Shopify.Domain.Core._common;
using Shopify.Domain.Core.CartAgg.AppService;
using Shopify.Domain.Core.CartAgg.Dto;
using Shopify.Domain.Core.CartAgg.Service;

namespace Shopify.Domain.AppService;

public class CartAppService(ICartService cartService) : ICartAppService
{
    public async Task<Result<CartDto>> GetUserCart(int userId, CancellationToken cancellationToken)
    {
        var cart =  await cartService.GetUserCart(userId, cancellationToken);
        if (cart == null)
            return Result<CartDto>.Failure("سبدی یافت نشد");
        return Result<CartDto>.Success(cart);
    }

    public async Task<Result<CartDto>> GetGuestCart(int guestId, CancellationToken cancellationToken)
    {
        var cart =  await cartService.GetGuestCart(guestId, cancellationToken);
        if (cart == null)
            return Result<CartDto>.Failure("سبدی یافت نشد");
        return Result<CartDto>.Success(cart);
    }

    public async Task<Result<bool>> ClearCart(int cartId, CancellationToken cancellationToken)
    {
        var result = await cartService.ClearCart(cartId, cancellationToken);
        if (!result)
            return Result<bool>.Failure("عملیات با خطا مواجه شد");
        return Result<bool>.Success(result);
    }

    public async Task<Result<bool>> MergeGuestCartToUser(int guestId, int userId, CancellationToken cancellationToken)
    {
        var result = await cartService.MergeGuestCartToUser(guestId, userId, cancellationToken);
        if (!result)
            return Result<bool>.Failure("عملیات با خطا مواجه شد");
        return Result<bool>.Success(result);
    }
}