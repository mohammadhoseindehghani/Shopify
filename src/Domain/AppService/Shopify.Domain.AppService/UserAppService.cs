using Shopify.Domain.Core.UserAgg.AppService;
using Shopify.Domain.Core.UserAgg.Service;

namespace Shopify.Domain.AppService;

public class UserAppService(IUserService userService) : IUserAppService
{
    
}