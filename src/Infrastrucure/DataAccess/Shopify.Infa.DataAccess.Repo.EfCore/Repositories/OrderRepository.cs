using Microsoft.EntityFrameworkCore;
using Shopify.Domain.Core.OrderAgg.Data;
using Shopify.Domain.Core.OrderAgg.Dto;
using Shopify.Domain.Core.OrderAgg.Entities;
using Shopify.Domain.Core.OrderAgg.Enums;
using Shopify.Infa.Db.SqlServer.EfCore.DbContexts;

namespace Shopify.Infa.DataAccess.Repo.EfCore.Repositories;

public class OrderRepository(AppDbContext context) : IOrderRepository
{
    public async Task Add(Order order, CancellationToken cancellationToken)
    {
        await context.Orders.AddAsync(order, cancellationToken);
    }

    public async Task CreateOrder(int userId, decimal totalAmount, List<OrderItemDto> items, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            UserId = userId,
            TotalAmount = totalAmount,
            Status = OrderStatusEnum.Processing,
            IsFinalized = true,
            OrderItems = items.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList()
        };

         await context.AddAsync(order, cancellationToken);
         await context.SaveChangesAsync(cancellationToken);
    }

    public async Task SaveChanges(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
    public async Task<OrderDto?> GetById(int id, CancellationToken cancellationToken)
    {
        return await context.Orders.Where(c => c.Id == id)
            .Select(c=>new OrderDto()
            {
                Id = c.Id,
                UserId = c.UserId,
                UserFirstName = c.User.FirstName,
                UserLastName = c.User.LastName,
                TotalAmount = c.TotalAmount,
                Status = c.Status,
                IsFinalized = c.IsFinalized,
                CreatedAt = c.CreatedAt,
                Items = c.OrderItems.Select(i=> new OrderItemDto()
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    ProductTitle = i.Product.Title,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<OrderDto?> GetUserActiveOrder(int userId, CancellationToken cancellationToken)
    {
        return await context.Orders.Where(c => c.UserId == userId && c.IsFinalized == false)
            .Select(c => new OrderDto()
            {
                Id = c.Id,
                UserId = c.UserId,
                UserFirstName = c.User.FirstName,
                UserLastName = c.User.LastName,
                TotalAmount = c.TotalAmount,
                Status = c.Status,
                IsFinalized = c.IsFinalized,
                CreatedAt = c.CreatedAt,
                Items = c.OrderItems.Select(i => new OrderItemDto()
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    ProductTitle = i.Product.Title,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ICollection<OrderDto>> GetUserOrders(int userId, CancellationToken cancellationToken)
    {
        return await context.Orders.Where(c => c.UserId == userId && c.IsFinalized == true)
            .Select(c => new OrderDto()
            {
                Id = c.Id,
                UserId = c.UserId,
                UserFirstName = c.User.FirstName,
                UserLastName = c.User.LastName,
                TotalAmount = c.TotalAmount,
                Status = c.Status,
                IsFinalized = c.IsFinalized,
                CreatedAt = c.CreatedAt,
                Items = c.OrderItems.Select(i => new OrderItemDto()
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    ProductTitle = i.Product.Title,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            })
            .ToListAsync(cancellationToken);

    }

    public async Task<ICollection<OrderDto>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Orders
            .Select(c => new OrderDto()
            {
                Id = c.Id,
                UserId = c.UserId,
                UserFirstName = c.User.FirstName,
                UserLastName = c.User.LastName,
                TotalAmount = c.TotalAmount,
                Status = c.Status,
                IsFinalized = c.IsFinalized,
                CreatedAt = c.CreatedAt,
                Items = c.OrderItems.Select(i => new OrderItemDto()
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    ProductTitle = i.Product.Title,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<ICollection<OrderDto>> GetOrdersByStatus(OrderStatusEnum status, CancellationToken cancellationToken)
    {
        return await context.Orders
            .Where(o=>o.Status == status)
            .Select(c => new OrderDto()
            {
                Id = c.Id,
                UserId = c.UserId,
                UserFirstName = c.User.FirstName,
                UserLastName = c.User.LastName,
                TotalAmount = c.TotalAmount,
                Status = c.Status,
                IsFinalized = c.IsFinalized,
                CreatedAt = c.CreatedAt,
                Items = c.OrderItems.Select(i => new OrderItemDto()
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    ProductTitle = i.Product.Title,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<ICollection<OrderItemDto>> GetOrderItems(int orderId, CancellationToken cancellationToken)
    {
        return await context.OrderItems
            .Where(oi => oi.OrderId == orderId)
            .Select(oi => new OrderItemDto
            {
                Id = oi.Id,
                ProductId = oi.ProductId,
                ProductTitle = oi.Product.Title,
                ProductImageUrl = oi.Product.ImageUrl, 
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<int> OrderCount(CancellationToken cancellationToken)
    {
        return await context.Orders.Where(o=>o.IsFinalized).CountAsync(cancellationToken);
    }

    public async Task<decimal> TotalOrder(CancellationToken cancellationToken)
    {
        return await context.Orders.Where(o => o.IsFinalized).SumAsync(p=>p.TotalAmount,cancellationToken);
    }
}