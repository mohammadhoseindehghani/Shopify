using Shopify.Domain.Core.CartAgg.Dto;
using Shopify.Domain.Core.CartAgg.Entities;

namespace Shopify.Domain.Core.CartAgg.Data;

public interface ICartRepository
{
    Task<CartDto?> GetUserCart(int userId, CancellationToken cancellationToken);
    Task<CartDto?> GetGuestCart(int guestId, CancellationToken cancellationToken);
    Task<bool> ClearCart(int cartId, CancellationToken cancellationToken);
    Task<bool> MergeGuestCartToUser(int guestId, int userId, CancellationToken cancellationToken);

}
