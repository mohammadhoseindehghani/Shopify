using Shopify.Domain.Core._common;
using Shopify.Domain.Core.CartAgg.Dto;

namespace Shopify.Domain.Core.CartAgg.AppService;

public interface ICartAppService
{
    Task<Result<CartDto>> GetUserCart(int userId, CancellationToken cancellationToken);
    Task<Result<CartDto>> GetGuestCart(int guestId, CancellationToken cancellationToken);
    Task<Result<bool>> ClearCart(int cartId, CancellationToken cancellationToken);
    Task<Result<bool>> MergeGuestCartToUser(int guestId, int userId, CancellationToken cancellationToken);
}