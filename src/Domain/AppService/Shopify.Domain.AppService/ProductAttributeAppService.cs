using Shopify.Domain.Core.ProductAgg.Entities;
using Shopify.Domain.Core.ProductAttributeAgg.AppService;
using Shopify.Domain.Core.ProductAttributeAgg.Service;

namespace Shopify.Domain.AppService;

public class ProductAttributeAppService(IProductAttributeService productAttributeService) : IProductAttributeAppService
{
    
}