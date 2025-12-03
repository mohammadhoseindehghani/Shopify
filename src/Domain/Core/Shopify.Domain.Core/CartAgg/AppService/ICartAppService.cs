using Shopify.Domain.Core._common;
using Shopify.Domain.Core.CartAgg.Dto;

namespace Shopify.Domain.Core.CartAgg.AppService;

public interface ICartAppService
{
    Task<Result<CartDto>> GetCart(int? userId, int? guestId, CancellationToken cancellationToken);
    Task<Result<int>> CreateCart(int? userId, int? guestId, CancellationToken cancellationToken);
    Task AddItemToCart(int cartId, int productId, int quantity, CancellationToken cancellationToken);
    Task UpdateItemQuantity(int cartId, int productId, int newQuantity, CancellationToken cancellationToken);
    Task RemoveItemFromCart(int cartId, int productId, CancellationToken cancellationToken);
    Task AssignCartToUser(int cartId, int userId, CancellationToken cancellationToken);
    Task DeleteCart(int cartId, CancellationToken cancellationToken);
    Task MergeCarts(int userId, int guestId, CancellationToken cancellationToken);


    Task<Result<CartDto>> GetUserCart(int userId, CancellationToken cancellationToken);
    Task<Result<CartDto>> GetGuestCart(int guestId, CancellationToken cancellationToken);
    Task<Result<bool>> ClearCart(int cartId, CancellationToken cancellationToken);
    Task<Result<bool>> MergeGuestCartToUser(int guestId, int userId, CancellationToken cancellationToken);
}