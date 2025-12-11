using Microsoft.EntityFrameworkCore;
using Shopify.Domain.Core.UserAgg.Data;
using Shopify.Domain.Core.UserAgg.Dto;
using Shopify.Domain.Core.UserAgg.Entities;
using Shopify.Domain.Core.UserAgg.Enums;
using Shopify.Infa.Db.SqlServer.EfCore.DbContexts;

namespace Shopify.Infa.DataAccess.Repo.EfCore.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task<UserDto?> GetById(int id, CancellationToken cancellationToken)
    {
        return await context.Users
            .Where(u => u.Id == id)
            .Select(u => new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Phone = u.Phone,
                ImgUrl = u.ImgUrl,
                IsActive = u.IsActive,
                Balance = u.Balance,
                Role = u.Role
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<UserDto?> GetByPhone(string phone, CancellationToken cansCancellationToken)
    {
        return await context.Users
            .Where(u => u.Phone == phone)
            .Select(u => new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Phone = u.Phone,
                ImgUrl = u.ImgUrl,
                IsActive = u.IsActive,
                Balance = u.Balance,
                Role = u.Role
            })
            .FirstOrDefaultAsync(cansCancellationToken);
    }

    public async Task<ICollection<UserDto>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Users
            .Select(u => new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Phone = u.Phone,
                ImgUrl = u.ImgUrl,
                IsActive = u.IsActive,
                Balance = u.Balance,
                Role = u.Role
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ChargeWallet(int userId, decimal amount, CancellationToken cancellationToken)
    {
        var effectedRows = await context.Users.Where(u => u.Id == userId)
            .ExecuteUpdateAsync(setter => setter
                .SetProperty(u => u.Balance, u => u.Balance + amount), cancellationToken);
        return effectedRows > 0;
    }

    public async Task<bool> Add(CreateUserDto userDto, CancellationToken cancellationToken)
    {
        var user = new User()
        {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Email = userDto.Email,
            Phone = userDto.Phone,
            PasswordHash = userDto.Password,
            ImgUrl = userDto.ImgUrl,
            Role = RoleEnum.Customer,
            IsActive = false
        };
        context.Users.Add(user);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> Active(int userId, CancellationToken cancellationToken)
    {
        var effectedRows = await context.Users.Where(u => u.Id == userId)
            .ExecuteUpdateAsync(setter => setter
                .SetProperty(u => u.IsActive, u => true), cancellationToken);
        return effectedRows > 0;
    }

    public async Task<bool> DeActive(int userId, CancellationToken cancellationToken)
    {
        var effectedRows = await context.Users.Where(u => u.Id == userId)
            .ExecuteUpdateAsync(setter => setter
                .SetProperty(u => u.IsActive, u => false), cancellationToken);
        return effectedRows > 0;
    }

    public async Task<UserWithPasswordDto?> GetByUserNameForLogin(string username, CancellationToken cancellationToken)
    {
        return await context.Users
            .Where(u => u.Phone == username)
            .Select(u => new UserWithPasswordDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Phone = u.Phone,
                ImgUrl = u.ImgUrl,
                IsActive = u.IsActive,
                Balance = u.Balance,
                Role = u.Role,
                PasswordHash = u.PasswordHash
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task DeductBalance(int userId, decimal amount, CancellationToken cancellationToken)
    {
        await context.Users
            .Where(u => u.Id == userId)
            .ExecuteUpdateAsync(
                setter => setter
                    .SetProperty(u => u.Balance, u => u.Balance - amount),
                cancellationToken
            );
    }

    public async Task<int> UserCount(CancellationToken cancellationToken)
    {
        return await context.Users.CountAsync(cancellationToken);
    }
}