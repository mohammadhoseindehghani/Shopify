using Shopify.Domain.Core.CartAgg.Data;
using Shopify.Infa.Db.SqlServer.EfCore.DbContexts;

namespace Shopify.Infa.DataAccess.Repo.EfCore.Repositories;

public class CartItemRepository(AppDbContext dbContext) : ICartItemRepository
{
    
}