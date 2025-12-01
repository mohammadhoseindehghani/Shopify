using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopify.Domain.Core.ProductAgg.Entities;

namespace Shopify.Infa.Db.SqlServer.EfCore.EntityConfigurations;

public class ProductAttributeValueConfigs : IEntityTypeConfiguration<ProductAttributeValue>
{
    public void Configure(EntityTypeBuilder<ProductAttributeValue> builder)
    {
        builder.ToTable("ProductAttributeValues");
        builder.HasKey(av => av.Id);

        builder.Property(av => av.Value)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.CreatedAt)
            .HasDefaultValueSql("GetDate()")
            .ValueGeneratedOnAdd();

        builder.HasOne(av => av.Product)
            .WithMany(p => p.AttributeValues)
            .HasForeignKey(av => av.ProductId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(av => av.ProductAttribute)
            .WithMany(pa => pa.AttributeValues)
            .HasForeignKey(av => av.ProductAttributeId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasData(
            new ProductAttributeValue
            {
                Id = 1,
                ProductId = 1,
                ProductAttributeId = 1, // Color
                Value = "Red",
                CreatedAt = new DateTime(2025, 11, 11),
            },
            new ProductAttributeValue
            {
                Id = 2,
                ProductId = 1,
                ProductAttributeId = 1, // Color
                Value = "Blue",
                CreatedAt = new DateTime(2025, 11, 11),
            },
            new ProductAttributeValue
            {
                Id = 3,
                ProductId = 1,
                ProductAttributeId = 2, // Size
                Value = "M",
                CreatedAt = new DateTime(2025, 11, 11),
            },
            new ProductAttributeValue
            {
                Id = 4,
                ProductId = 1,
                ProductAttributeId = 2, // Size
                Value = "L",
                CreatedAt = new DateTime(2025, 11, 11),
            },
            new ProductAttributeValue
            {
                Id = 5,
                ProductId = 2,
                ProductAttributeId = 5, // Model
                Value = "XPS 13",
                CreatedAt = new DateTime(2025, 11, 11),
            },
            new ProductAttributeValue
            {
                Id = 6,
                ProductId = 2,
                ProductAttributeId = 3, // Weight
                Value = "1.2kg",
                CreatedAt = new DateTime(2025, 11, 11),
            }
        );

    }
}