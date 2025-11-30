using Shopify.Domain.Core.CartAgg.AppService;
using Shopify.Domain.Core.CartAgg.Service;

namespace Shopify.Domain.AppService;

public class CartAppService(ICartService cartService) : ICartAppService
{
    
}