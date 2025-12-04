using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopify.Domain.AppService;
using Shopify.Domain.Core.CategoryAgg.AppService;
using Shopify.Domain.Core.CategoryAgg.Dto;
using Shopify.Domain.Core.ProductAgg.AppService;
using Shopify.Domain.Core.ProductAgg.Dto;
using Shopify.Domain.Core.ProductAgg.Entities;

namespace Shopify.Presentation.RazorPages.Pages
{
    public class IndexModel(IProductAppService productAppService, ICategoryAppService categoryAppService) : PageModel
    {
        public ICollection<CategoryDto> Categories { get; set; }
        public ICollection<ProductListDto> ProductList { get; set; }
        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            Categories = await categoryAppService.GetAll(cancellationToken);
            ProductList = await productAppService.GetActiveProducts(cancellationToken);
        }

        public async Task OnGetCategoryFilterPostAsync(int productId, CancellationToken cancellationToken)
        {
            Categories = await categoryAppService.GetAll( cancellationToken);
            ProductList = await productAppService.GetProductsByCategory(productId, cancellationToken);
        }
    }
}
