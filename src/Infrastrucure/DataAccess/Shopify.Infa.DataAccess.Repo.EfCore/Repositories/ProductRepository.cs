using Shopify.Domain.Core.ProductAgg.Data;
using Shopify.Infa.Db.SqlServer.EfCore.DbContexts;

namespace Shopify.Infa.DataAccess.Repo.EfCore.Repositories;

public class ProductRepository(AppDbContext context) : IProductRepository
{
    
}