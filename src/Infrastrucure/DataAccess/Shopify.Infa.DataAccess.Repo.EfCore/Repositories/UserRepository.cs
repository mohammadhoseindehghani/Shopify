using Shopify.Domain.Core.UserAgg.Data;
using Shopify.Infa.Db.SqlServer.EfCore.DbContexts;

namespace Shopify.Infa.DataAccess.Repo.EfCore.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    
}