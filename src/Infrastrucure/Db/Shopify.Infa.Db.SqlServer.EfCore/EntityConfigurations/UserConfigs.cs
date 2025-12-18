using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopify.Domain.Core.UserAgg.Entities;
using Shopify.Domain.Core.UserAgg.Enums;
using System.Collections.Generic;

namespace Shopify.Infa.Db.SqlServer.EfCore.EntityConfigurations;

public class UserConfigs : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);

        builder.Property(u => u.PhoneNumber)
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



        var hasher = new PasswordHasher<User>();

        builder.HasData(
            new User
            {
                Id = 1,
                UserName = "09120000001",
                NormalizedUserName = "09120000001",
                PhoneNumber = "09120000001",
                PhoneNumberConfirmed = true,
                Email = "admin@shop.com",
                NormalizedEmail = "ADMIN@SHOP.COM",
                EmailConfirmed = true,

                PasswordHash = hasher.HashPassword(null, "123456789"),
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ConcurrencyStamp = Guid.NewGuid().ToString("D"),
                LockoutEnabled = false,

                FirstName = "Admin",
                LastName = "User",
                ImgUrl = "default.jpg",
                CreatedAt = new DateTime(2025, 11, 11)
            },

            new User
            {
                Id = 2,
                UserName = "09120000002",
                NormalizedUserName = "09120000002",
                PhoneNumber = "09120000002",
                PhoneNumberConfirmed = true,
                Email = "operator@shop.com",
                NormalizedEmail = "OPERATOR@SHOP.COM",
                EmailConfirmed = true,

                PasswordHash = hasher.HashPassword(null, "123456789"),
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ConcurrencyStamp = Guid.NewGuid().ToString("D"),
                LockoutEnabled = false,

                FirstName = "Operator",
                LastName = "User",
                ImgUrl = "default.jpg",
                CreatedAt = new DateTime(2025, 11, 11)
            },

            new User
            {
                Id = 3,
                UserName = "09120000003",
                NormalizedUserName = "09120000003",
                PhoneNumber = "09120000003",
                PhoneNumberConfirmed = true,
                Email = "user1@gmail.com",
                NormalizedEmail = "USER1@GMAIL.COM",
                EmailConfirmed = true,

                PasswordHash = hasher.HashPassword(null, "123456789"),
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ConcurrencyStamp = Guid.NewGuid().ToString("D"),
                LockoutEnabled = false,

                FirstName = "Ali",
                LastName = "Ahmadi",
                ImgUrl = "default.jpg",
                CreatedAt = new DateTime(2025, 11, 11)
            },

            new User
            {
                Id = 4,
                UserName = "09120000004",
                NormalizedUserName = "09120000004",
                PhoneNumber = "09120000004",
                PhoneNumberConfirmed = true,
                Email = "user2@gmail.com",
                NormalizedEmail = "USER2@GMAIL.COM",
                EmailConfirmed = true,

                PasswordHash = hasher.HashPassword(null, "123456789"),
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ConcurrencyStamp = Guid.NewGuid().ToString("D"),
                LockoutEnabled = false,

                FirstName = "Reza",
                LastName = "Moradi",
                ImgUrl = "default.jpg",
                CreatedAt = new DateTime(2025, 11, 11)
            },

            new User
            {
                Id = 5,
                UserName = "09120000005",
                NormalizedUserName = "09120000005",
                PhoneNumber = "09120000005",
                PhoneNumberConfirmed = true,
                Email = "user3@gmail.com",
                NormalizedEmail = "USER3@GMAIL.COM",
                EmailConfirmed = true,

                PasswordHash = hasher.HashPassword(null, "123456789"),
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ConcurrencyStamp = Guid.NewGuid().ToString("D"),
                LockoutEnabled = false,

                FirstName = "Sara",
                LastName = "Karimi",
                ImgUrl = "default.jpg",
                CreatedAt = new DateTime(2025, 11, 11)
            }
        );

    }
}