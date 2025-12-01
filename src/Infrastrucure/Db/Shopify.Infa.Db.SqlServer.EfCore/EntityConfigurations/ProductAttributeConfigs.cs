using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopify.Domain.Core.ProductAttributeAgg.Entities;

namespace Shopify.Infa.Db.SqlServer.EfCore.EntityConfigurations;

public class ProductAttributeConfigs : IEntityTypeConfiguration<ProductAttribute>
{
    public void Configure(EntityTypeBuilder<ProductAttribute> builder)
    {
        builder.ToTable("ProductAttributes");

        builder.Property(c => c.CreatedAt)
            .HasDefaultValueSql("GetDate()")
            .ValueGeneratedOnAdd();

        builder.HasKey(pa => pa.Id);
        builder.Property(pa => pa.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasData(
            new ProductAttribute { Id = 1, Name = "Color", CreatedAt = new DateTime(2025, 11, 11), },
            new ProductAttribute { Id = 2, Name = "Size", CreatedAt = new DateTime(2025, 11, 11), },
            new ProductAttribute { Id = 3, Name = "Weight", CreatedAt = new DateTime(2025, 11, 11), },
            new ProductAttribute { Id = 4, Name = "Material", CreatedAt = new DateTime(2025, 11, 11), },
            new ProductAttribute { Id = 5, Name = "Model", CreatedAt = new DateTime(2025, 11, 11), }
        );

    }
}