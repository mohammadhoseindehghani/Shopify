using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shopify.Domain.Core.ProductAttributeAgg.Data;
using Shopify.Domain.Core.ProductAttributeAgg.Dto;
using Shopify.Domain.Core.ProductAttributeAgg.Entities;
using Shopify.Infa.Db.SqlServer.EfCore.DbContexts;

namespace Shopify.Infa.DataAccess.Repo.EfCore.Repositories;

public class ProductAttributeRepository(AppDbContext context) : IProductAttributeRepository
{
    public async Task<bool> Add(string name, CancellationToken cancellationToken)
    {
        var attribute = new ProductAttribute()
        {
            Name = name
        };
        context.Add(attribute);
        return await context.SaveChangesAsync(cancellationToken) > 0;

    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        var effectedRows = await context.ProductAttributes
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
        return effectedRows > 0;
    }

    public async Task<ICollection<ProductAttributeDto>> GetAll(CancellationToken cancellationToken)
    {
        return await context.ProductAttributes.Select(a => new ProductAttributeDto()
        {
            Name = a.Name,
            Id = a.Id
        }).ToListAsync(cancellationToken);
    }

    public async Task<bool> Update(int id, string name, CancellationToken cancellationToken)
    {
        var effectedRows = await context.ProductAttributes.Where(a => a.Id == id)
            .ExecuteUpdateAsync(setter => setter
                .SetProperty(a => a.Name, name), cancellationToken);

        return effectedRows > 0;
    }

    public async Task<bool> ExistsByName(string name, CancellationToken cancellationToken)
    {
        return await context.ProductAttributes.AnyAsync(a => a.Name == name, cancellationToken);
    }
}