using Shopify.Domain.Core.UserAgg.Data;
using Shopify.Domain.Core.UserAgg.Service;

namespace Shopify.Domain.Service;

public class UserService(IUserRepository userRepository) : IUserService
{
    
}