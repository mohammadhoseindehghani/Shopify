using Microsoft.EntityFrameworkCore;
using Shopify.Domain.Core.CartAgg.Data;
using Shopify.Domain.Core.CartAgg.Dto;
using Shopify.Infa.Db.SqlServer.EfCore.DbContexts;

namespace Shopify.Infa.DataAccess.Repo.EfCore.Repositories;

public class CartRepository (AppDbContext context) : ICartRepository
{
    public async Task<CartDto?> GetUserCart(int userId, CancellationToken cancellationToken)
    {
        return await context.Carts.Where(c => c.UserId == userId)
            .Select(c => new CartDto()
            {
                Id = c.Id,
                UserId = c.UserId,
                GuestId = c.GuestId,
                Items = c.Items.Select(i => new CartItemDto()
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()

            }).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<CartDto?> GetGuestCart(int guestId, CancellationToken cancellationToken)
    {
        return await context.Carts.Where(c => c.GuestId == guestId)
            .Select(c => new CartDto()
            {
                Id = c.Id,
                UserId = c.UserId,
                GuestId = c.GuestId,
                Items = c.Items.Select(i => new CartItemDto()
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
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