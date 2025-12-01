using Microsoft.EntityFrameworkCore;
using Shopify.Domain.Core.UserAgg.Data;
using Shopify.Domain.Core.UserAgg.Dto;
using Shopify.Infa.Db.SqlServer.EfCore.DbContexts;

namespace Shopify.Infa.DataAccess.Repo.EfCore.Repositories;

public class AddressRepository(AppDbContext context) : IAddressRepository
{
    public async Task<AddressDto?> GetByUserId(int userId, CancellationToken cancellationToken)
    {
        return await context.Addresses
            .Where(a => a.UserId == userId)
            .Select(a => new AddressDto()
            {
                Id = a.Id,
                Name = a.Name,
                Province = a.Province,
                City = a.City,
                Street = a.Street,
                Plaque = a.Plaque,
                UnitNumber = a.UnitNumber,
                PostalCode = a.PostalCode,
                IsDefault = a.IsDefault
            }).FirstOrDefaultAsync(cancellationToken);
    }
}