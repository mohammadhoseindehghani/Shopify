using Shopify.Domain.Core.OrderAgg.AppService;
using Shopify.Domain.Core.OrderAgg.Service;

namespace Shopify.Domain.AppService;

public class OrderAppService(IOrderService orderService) : IOrderAppService
{
    
}