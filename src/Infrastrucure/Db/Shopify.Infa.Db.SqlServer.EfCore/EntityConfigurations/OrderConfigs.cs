using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopify.Domain.Core.OrderAgg.Entities;
using Shopify.Domain.Core.OrderAgg.Enums;

namespace Shopify.Infa.Db.SqlServer.EfCore.EntityConfigurations;

public class OrderConfigs : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.CreatedAt)
            .HasDefaultValueSql("GetDate()")
            .ValueGeneratedOnAdd();

        builder.Property(o => o.TotalAmount)
            .HasColumnType("decimal(18,0)")
            .IsRequired();

        builder.HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasData(
            new Order
            {
                Id = 1,
                UserId = 3, // User1
                Status = OrderStatusEnum.Processing,
                IsFinalized = true,
                TotalAmount = 28000000, 
                CreatedAt = DateTime.UtcNow
            },
            new Order
            {
                Id = 2,
                UserId = 4, // User2
                Status = OrderStatusEnum.Completed,
                IsFinalized = true,
                TotalAmount = 15500000,
                CreatedAt = DateTime.UtcNow
            },
            new Order
            {
                Id = 3,
                UserId = 5, // User3
                Status = OrderStatusEnum.Processing,
                IsFinalized = false,
                TotalAmount = 9000000,
                CreatedAt = DateTime.UtcNow
            }
        );

    }
}