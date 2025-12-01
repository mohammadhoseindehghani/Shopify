using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopify.Domain.Core.UserAgg.Entities;

namespace Shopify.Infa.Db.SqlServer.EfCore.EntityConfigurations;

public class AddressConfigs : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("Addresses");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(a => a.Province)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(a => a.City)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(a => a.Street)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.CreatedAt)
            .HasDefaultValueSql("GetDate()")
            .ValueGeneratedOnAdd();

        builder.Property(a => a.PostalCode)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(a => a.IsDefault)
            .IsRequired();

        builder.HasOne(a => a.User)
            .WithMany(u => u.Addresses)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.NoAction);


        builder.HasData(
            new Address
            {
                Id = 1,
                UserId = 1, // Admin
                Name = "Admin Home",
                Province = "Tehran",
                City = "Tehran",
                Street = "Valiasr Street, No. 120",
                PostalCode = "1111111111",
                IsDefault = true,
                CreatedAt = new DateTime(2025, 11, 11),
            },
            new Address
            {
                Id = 2,
                UserId = 2, // Operator
                Name = "Operator Office",
                Province = "Tehran",
                City = "Karaj",
                Street = "Azadi Blvd, No. 45",
                PostalCode = "2222222222",
                IsDefault = true,
                CreatedAt = new DateTime(2025, 11, 11),
            },
            new Address
            {
                Id = 3,
                UserId = 3, // Normal user 1
                Name = "Home",
                Province = "Isfahan",
                City = "Isfahan",
                Street = "Chaharbagh Abbasi, No. 87",
                PostalCode = "3333333333",
                IsDefault = true,
                CreatedAt = new DateTime(2025, 11, 11),
            },
            new Address
            {
                Id = 4,
                UserId = 4, // Normal user 2
                Name = "Home",
                Province = "Shiraz",
                City = "Shiraz",
                Street = "Zand Street, No. 51",
                PostalCode = "4444444444",
                IsDefault = true,
                CreatedAt = new DateTime(2025, 11, 11),
            },
            new Address
            {
                Id = 5,
                UserId = 5, // Normal user 3
                Name = "Home",
                Province = "Tabriz",
                City = "Tabriz",
                Street = "Shariati Street, No. 20",
                PostalCode = "5555555555",
                IsDefault = true,
                CreatedAt = new DateTime(2025, 11, 11),
            }
        );


    }
}