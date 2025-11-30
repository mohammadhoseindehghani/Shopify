using Shopify.Domain.Core.ProductAgg.AppService;
using Shopify.Domain.Core.ProductAgg.Service;

namespace Shopify.Domain.AppService;

public class ProductAppService(IProductService productService) : IProductAppService
{
    
}