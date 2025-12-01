using Microsoft.EntityFrameworkCore;
using Shopify.Domain.Core.UserAgg.Data;
using Shopify.Domain.Core.UserAgg.Dto;
using Shopify.Domain.Core.UserAgg.Entities;
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
                Balance = u.Balance,
                Role = u.Role
            })
            .FirstOrDefaultAsync(cansCancellationToken);
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
                Balance = u.Balance,
                Role = u.Role,
                PasswordHash = u.PasswordHash
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}