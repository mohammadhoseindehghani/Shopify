using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopify.Domain.Core.CategoryAgg.Entities;

namespace Shopify.Infa.Db.SqlServer.EfCore.EntityConfigurations;

public class CategoryConfigs : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.CreatedAt)
            .HasDefaultValueSql("GetDate()")
            .ValueGeneratedOnAdd();

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasData(
            new Category
            {
                Id = 1,
                Name = "Electronics",
                ParentId = null,
                CreatedAt = DateTime.UtcNow
            },
            new Category
            {
                Id = 2,
                Name = "Laptop",
                ParentId = 1,
                CreatedAt = DateTime.UtcNow
            },
            new Category
            {
                Id = 3,
                Name = "Mobile",
                ParentId = 1,
                CreatedAt = DateTime.UtcNow
            },
            new Category
            {
                Id = 4,
                Name = "Headphones",
                ParentId = 1,
                CreatedAt = DateTime.UtcNow
            },
            new Category
            {
                Id = 5,
                Name = "Smart Watch",
                ParentId = 1,
                CreatedAt = DateTime.UtcNow
            }
        );



    }
}