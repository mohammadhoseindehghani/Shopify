using Shopify.Domain.Core._common;
using Shopify.Domain.Core.CartAgg.AppService;
using Shopify.Domain.Core.CartAgg.Dto;
using Shopify.Domain.Core.CartAgg.Service;

namespace Shopify.Domain.AppService;

public class CartAppService(ICartService cartService) : ICartAppService
{
    public async Task<Result<CartDto>> GetCart(int? userId, int? guestId, CancellationToken cancellationToken)
    {
        var cart = await cartService.GetCart(userId, guestId, cancellationToken);
        if (cart == null)
        {
            return Result<CartDto>.Failure("این کارت یافت نشد");
        }

        return Result<CartDto>.Success(cart);
    }

    public async Task<Result<int>> CreateCart(int? userId, int? guestId, CancellationToken cancellationToken)
    {
        var result = await cartService.CreateCart(userId, guestId, cancellationToken);
        if (result == 0)
        {
            return Result<int>.Failure("ساخت سبد خرید با شکست مواحه شد");
        }

        return Result<int>.Success(result);
    }

    public async Task AddItemToCart(int cartId, int productId, int quantity, CancellationToken cancellationToken)
    {
        await cartService.AddItemToCart(cartId, productId, quantity, cancellationToken);
    }

    public async Task UpdateItemQuantity(int cartId, int productId, int newQuantity, CancellationToken cancellationToken)
    {
        await cartService.UpdateItemQuantity(cartId, productId, newQuantity, cancellationToken);
    }

    public async Task RemoveItemFromCart(int cartId, int productId, CancellationToken cancellationToken)
    {
        await cartService.RemoveItemFromCart(cartId, productId, cancellationToken);
    }

    public async Task AssignCartToUser(int cartId, int userId, CancellationToken cancellationToken)
    {
        await cartService.AssignCartToUser(cartId, userId, cancellationToken);
    }

    public async Task DeleteCart(int cartId, CancellationToken cancellationToken)
    {
        await cartService.DeleteCart(cartId, cancellationToken);
    }

    public async Task MergeCarts(int userId, int guestId, CancellationToken cancellationToken)
    {
        await cartService.MergeCarts(userId, guestId, cancellationToken);
    }

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