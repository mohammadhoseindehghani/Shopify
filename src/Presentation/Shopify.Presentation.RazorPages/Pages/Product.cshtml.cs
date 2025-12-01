using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopify.Domain.Core.ProductAgg.AppService;
using Shopify.Domain.Core.ProductAgg.Dto;

namespace Shopify.Presentation.RazorPages.Pages
{
    public class ProductModel(IProductAppService productAppService) : PageModel
    {
        public ProductDetailDto ProductDetail { get; set; }
        public async Task OnGet(int id, CancellationToken cancellationToken)
        {
            var result  = await productAppService.GetById(id, cancellationToken);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("",result.Message!);
            }

            ProductDetail = result.Data!;
        }
    }
}
