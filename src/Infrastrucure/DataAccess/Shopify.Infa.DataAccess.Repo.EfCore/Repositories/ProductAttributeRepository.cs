using Shopify.Domain.Core.ProductAttributeAgg.Data;
using Shopify.Infa.Db.SqlServer.EfCore.DbContexts;

namespace Shopify.Infa.DataAccess.Repo.EfCore.Repositories;

public class ProductAttributeRepository(AppDbContext context) : IProductAttributeRepository
{
    
}