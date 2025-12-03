using Microsoft.EntityFrameworkCore;
using Shopify.Domain.Core.CartAgg.Data;
using Shopify.Domain.Core.CartAgg.Dto;
using Shopify.Domain.Core.CartAgg.Entities;
using Shopify.Infa.Db.SqlServer.EfCore.DbContexts;

namespace Shopify.Infa.DataAccess.Repo.EfCore.Repositories;

public class CartRepository (AppDbContext context) : ICartRepository
{
    public async Task<CartDto?> GetCart(int? userId, int? guestId, CancellationToken cancellationToken)
    {
        var query = context.Carts.AsQueryable();

        if (userId.HasValue) query = query.Where(c => c.UserId == userId);
        else if (guestId.HasValue) query = query.Where(c => c.GuestId == guestId);
        else return null!;

        return await query.Select(c => new CartDto
        {
            Id = c.Id,
            Items = c.Items.Select(i => new CartItemDto
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                ProductTitle = i.Product.Title,
                ImageUrl = i.Product.ImageUrl,
                UnitPrice = i.Product.Price
            }).ToList()
        }).FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<int> CreateCart(int? userId, int? guestId, CancellationToken cancellationToken)
    {
        var cart = new Cart { UserId = userId, GuestId = guestId, CreatedAt = DateTime.Now };
        context.Carts.Add(cart);
        await context.SaveChangesAsync(cancellationToken);
        return cart.Id;
    }

    public async Task AddItemToCart(int cartId, int productId, int quantity,CancellationToken cancellationToken)
    {
        var item = new CartItem { CartId = cartId, ProductId = productId, Quantity = quantity };
        context.CartItems.Add(item);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateItemQuantity(int cartId, int productId, int newQuantity,CancellationToken cancellationToken)
    {

        var item = await context.CartItems
            .FirstOrDefaultAsync(i => i.CartId == cartId && i.ProductId == productId, cancellationToken: cancellationToken);

        if (item != null)
        {
            item.Quantity = newQuantity;
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task RemoveItemFromCart(int cartId, int productId,CancellationToken cancellationToken)
    {
        var item = await context.CartItems
            .FirstOrDefaultAsync(i => i.CartId == cartId && i.ProductId == productId, cancellationToken: cancellationToken);

        if (item != null)
        {
            context.CartItems.Remove(item);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task AssignCartToUser(int cartId, int userId, CancellationToken cancellationToken)
    {
        var cart = await context.Carts.FirstOrDefaultAsync(c=>c.Id == cartId, cancellationToken);
        if (cart != null)
        {
            cart.UserId = userId;
            cart.GuestId = null; 
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task DeleteCart(int cartId, CancellationToken cancellationToken)
    {
        var cart = await context.Carts.FirstOrDefaultAsync(c=>c.Id == cartId ,cancellationToken);
        if (cart != null)
        {
            context.Carts.Remove(cart);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<CartDto?> GetUserCart(int userId, CancellationToken cancellationToken)
    {
        return await context.Carts.Where(c => c.UserId == userId)
            .Select(c => new CartDto()
            {
                Id = c.Id,
                Items = c.Items.Select(i => new CartItemDto()
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    ProductTitle = i.Product.Title,
                    ImageUrl = i.Product.ImageUrl,
                    UnitPrice = i.Product.Price
                }).ToList()

            }).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<CartDto?> GetGuestCart(int guestId, CancellationToken cancellationToken)
    {
        return await context.Carts.Where(c => c.GuestId == guestId)
            .Select(c => new CartDto()
            {
                Id = c.Id,
                Items = c.Items.Select(i => new CartItemDto()
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    ProductTitle = i.Product.Title,
                    ImageUrl = i.Product.ImageUrl,
                    UnitPrice = i.Product.Price
                }).ToList()
            }).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> ClearCart(int cartId, CancellationToken cancellationToken)
    {
        var cart = await context.Carts.Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.Id == cartId, cancellationToken);
        if (cart == null)
            return false;
        context.CartItems.RemoveRange(cart.Items);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public Task<bool> MergeGuestCartToUser(int guestId, int userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}