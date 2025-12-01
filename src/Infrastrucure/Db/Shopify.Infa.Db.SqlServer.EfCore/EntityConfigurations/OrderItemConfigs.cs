using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopify.Domain.Core.OrderAgg.Entities;

namespace Shopify.Infa.Db.SqlServer.EfCore.EntityConfigurations;

public class OrderItemConfigs : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");

        builder.HasKey(oi => oi.Id);

        builder.Property(c => c.CreatedAt)
            .HasDefaultValueSql("GetDate()")
            .ValueGeneratedOnAdd();

        builder.Property(oi => oi.Quantity)
            .IsRequired();

        builder.Property(oi => oi.UnitPrice)
            .HasColumnType("decimal(18,0)")
            .IsRequired();

        builder.HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oi => oi.Product)
            .WithMany()
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.NoAction);


        builder.HasData(
            // Order 1 items
            new OrderItem
            {
                Id = 1,
                OrderId = 1,
                ProductId = 1,
                Quantity = 1,
                UnitPrice = 20000000
            },
            new OrderItem
            {
                Id = 2,
                OrderId = 1,
                ProductId = 4,
                Quantity = 1,
                UnitPrice = 8000000
            },

            // Order 2 items
            new OrderItem
            {
                Id = 3,
                OrderId = 2,
                ProductId = 2,
                Quantity = 1,
                UnitPrice = 15000000
            },
            new OrderItem
            {
                Id = 4,
                OrderId = 2,
                ProductId = 10,
                Quantity = 1,
                UnitPrice = 500000
            },

            // Order 3 items
            new OrderItem
            {
                Id = 5,
                OrderId = 3,
                ProductId = 3,
                Quantity = 1,
                UnitPrice = 12000000
            },
            new OrderItem
            {
                Id = 6,
                OrderId = 3,
                ProductId = 15,
                Quantity = 1,
                UnitPrice = 3000000
            }
        );

    }
}
