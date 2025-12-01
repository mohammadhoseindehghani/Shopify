using Shopify.Domain.Core.CartAgg.Data;
using Shopify.Domain.Core.CartAgg.Dto;
using Shopify.Domain.Core.CartAgg.Service;

namespace Shopify.Domain.Service;

public class CartService(ICartRepository cartRepository) : ICartService
{
    public async Task<CartDto?> GetUserCart(int userId, CancellationToken cancellationToken)
    {
        return await cartRepository.GetUserCart(userId, cancellationToken);
    }

    public async Task<CartDto?> GetGuestCart(int guestId, CancellationToken cancellationToken)
    {
        return await cartRepository.GetGuestCart(guestId, cancellationToken);
    }

    public Task<bool> ClearCart(int cartId, CancellationToken cancellationToken)
    {
        return cartRepository.ClearCart(cartId, cancellationToken);
    }

    public Task<bool> MergeGuestCartToUser(int guestId, int userId, CancellationToken cancellationToken)
    {
        return cartRepository.MergeGuestCartToUser(guestId, userId, cancellationToken);
    }
}