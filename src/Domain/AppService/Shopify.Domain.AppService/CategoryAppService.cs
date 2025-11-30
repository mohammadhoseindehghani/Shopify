using Shopify.Domain.Core.CategoryAgg.AppService;
using Shopify.Domain.Core.CategoryAgg.Service;

namespace Shopify.Domain.AppService;

public class CategoryAppService(ICategoryService categoryService) : ICategoryAppService
{
    
}