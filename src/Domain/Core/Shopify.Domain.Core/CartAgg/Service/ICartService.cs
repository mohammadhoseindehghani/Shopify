using Shopify.Domain.Core.CartAgg.Dto;

namespace Shopify.Domain.Core.CartAgg.Service;

public interface ICartService
{
    Task<CartDto?> GetUserCart(int userId, CancellationToken cancellationToken);
    Task<CartDto?> GetGuestCart(int guestId, CancellationToken cancellationToken);
    Task<bool> ClearCart(int cartId, CancellationToken cancellationToken);
    Task<bool> MergeGuestCartToUser(int guestId, int userId, CancellationToken cancellationToken);
}