using Shopify.Domain.Core.CartAgg.Dto;
using Shopify.Domain.Core.CartAgg.Entities;

namespace Shopify.Domain.Core.CartAgg.Data;

public interface ICartRepository
{
    Task<CartDto?> GetCart(int? userId, int? guestId, CancellationToken cancellationToken);
    Task<int> CreateCart(int? userId, int? guestId, CancellationToken cancellationToken); 
    Task AddItemToCart(int cartId, int productId, int quantity, CancellationToken cancellationToken);
    Task UpdateItemQuantity(int cartId, int productId, int newQuantity, CancellationToken cancellationToken);
    Task RemoveItemFromCart(int cartId, int productId, CancellationToken cancellationToken);
    Task AssignCartToUser(int cartId, int userId,CancellationToken cancellationToken);
    Task DeleteCart(int cartId, CancellationToken cancellationToken);


    Task<CartDto?> GetUserCart(int userId, CancellationToken cancellationToken);
    Task<CartDto?> GetGuestCart(int guestId, CancellationToken cancellationToken);
    Task<bool> ClearCart(int cartId, CancellationToken cancellationToken);
    Task<bool> MergeGuestCartToUser(int guestId, int userId, CancellationToken cancellationToken);

}
