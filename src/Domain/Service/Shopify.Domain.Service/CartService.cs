using Shopify.Domain.Core.CartAgg.Data;
using Shopify.Domain.Core.CartAgg.Dto;
using Shopify.Domain.Core.CartAgg.Service;

namespace Shopify.Domain.Service;

public class CartService(ICartRepository cartRepository) : ICartService
{
    public async Task<CartDto?> GetCart(int? userId, int? guestId, CancellationToken cancellationToken)
    {
        return await cartRepository.GetCart(userId, guestId, cancellationToken);
    }

    public async Task<int> CreateCart(int? userId, int? guestId, CancellationToken cancellationToken)
    {
        return await cartRepository.CreateCart(userId, guestId, cancellationToken);
    }

    public async Task AddItemToCart(int cartId, int productId, int quantity, CancellationToken cancellationToken)
    {
           await cartRepository.AddItemToCart(cartId, productId, quantity, cancellationToken);
    }

    public async Task UpdateItemQuantity(int cartId, int productId, int newQuantity, CancellationToken cancellationToken)
    {
        await cartRepository.UpdateItemQuantity(cartId, productId, newQuantity, cancellationToken);
    }

    public async Task RemoveItemFromCart(int cartId, int productId, CancellationToken cancellationToken)
    {
        await cartRepository.RemoveItemFromCart(cartId, productId, cancellationToken);
    }

    public async Task AssignCartToUser(int cartId, int userId, CancellationToken cancellationToken)
    {
        await cartRepository.AssignCartToUser(cartId, userId, cancellationToken);
    }

    public async Task DeleteCart(int cartId, CancellationToken cancellationToken)
    {
        await cartRepository.DeleteCart(cartId, cancellationToken);
    }

    public async Task MergeCarts(int userId, int guestId,CancellationToken cancellationToken)
    {
     
        var guestCartDto = await cartRepository.GetCart(null, guestId,cancellationToken);
        if (guestCartDto == null) return;

        var userCartDto = await cartRepository.GetCart(userId, null,cancellationToken);

        if (userCartDto == null)
        {
            await cartRepository.AssignCartToUser(guestCartDto.Id, userId,cancellationToken);
        }
        else
        {

            foreach (var guestItem in guestCartDto.Items)
            {
                var userItem = userCartDto.Items.FirstOrDefault(i => i.ProductId == guestItem.ProductId);

                if (userItem != null)
                {

                    int newQuantity = userItem.Quantity + guestItem.Quantity;
                    await cartRepository.UpdateItemQuantity(userCartDto.Id, guestItem.ProductId, newQuantity,cancellationToken);
                }
                else
                {
                    await cartRepository.AddItemToCart(userCartDto.Id, guestItem.ProductId, guestItem.Quantity,cancellationToken);
                }
            }
            await cartRepository.DeleteCart(guestCartDto.Id,cancellationToken);
        }
    }

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