using Shopify.Domain.Core.OrderAgg.Data;
using Shopify.Infa.Db.SqlServer.EfCore.DbContexts;

namespace Shopify.Infa.DataAccess.Repo.EfCore.Repositories;

public class OrderRepository(AppDbContext context) : IOrderRepository
{
    
}