using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopify.Domain.Core.UserAgg.Entities;
using Shopify.Domain.Core.UserAgg.Enums;

namespace Shopify.Infa.Db.SqlServer.EfCore.EntityConfigurations;

public class UserConfigs : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Phone)
            .IsRequired()
            .HasMaxLength(20);
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasMaxLength(256);
        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.CreatedAt)
            .HasDefaultValueSql("GetDate()")
            .ValueGeneratedOnAdd();

        builder.HasMany(u => u.Addresses)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(u => u.Orders)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.NoAction);


        builder.HasData(
            new User
            {
                Id = 1,
                Phone = "09120000001",
                Email = "admin@shop.com",
                PasswordHash = "Ntbi9dzykpCIkY2SS2CsAA==:1ILjnLtYlBsO6QJDJ4qOlh7Ul7z1ws3SIBUEW62MEjU=", //123456789
                FirstName = "Admin",
                LastName = "User",
                Role = RoleEnum.Admin
            },
            new User
            {
                Id = 2,
                Phone = "09120000002",
                Email = "operator@shop.com",
                PasswordHash = "Ntbi9dzykpCIkY2SS2CsAA==:1ILjnLtYlBsO6QJDJ4qOlh7Ul7z1ws3SIBUEW62MEjU=",
                FirstName = "Operator",
                LastName = "User",
                Role = RoleEnum.Operator
            },
            new User
            {
                Id = 3,
                Phone = "09120000003",
                Email = "user1@gmail.com",
                PasswordHash = "Ntbi9dzykpCIkY2SS2CsAA==:1ILjnLtYlBsO6QJDJ4qOlh7Ul7z1ws3SIBUEW62MEjU=",
                FirstName = "Ali",
                LastName = "Ahmadi",
                Role = RoleEnum.Customer
            },
            new User
            {
                Id = 4,
                Phone = "09120000004",
                Email = "user2@gmail.com",
                PasswordHash = "Ntbi9dzykpCIkY2SS2CsAA==:1ILjnLtYlBsO6QJDJ4qOlh7Ul7z1ws3SIBUEW62MEjU=",
                FirstName = "Reza",
                LastName = "Moradi",
                Role = RoleEnum.Customer
            },
            new User
            {
                Id = 5,
                Phone = "09120000005",
                Email = "user3@gmail.com",
                PasswordHash = "Ntbi9dzykpCIkY2SS2CsAA==:1ILjnLtYlBsO6QJDJ4qOlh7Ul7z1ws3SIBUEW62MEjU=",
                FirstName = "Sara",
                LastName = "Karimi",
                Role = RoleEnum.Customer
            }
        );

    }
}