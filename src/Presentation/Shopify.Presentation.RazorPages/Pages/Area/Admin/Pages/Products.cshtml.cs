using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shopify.Domain.Core.CategoryAgg.AppService;
using Shopify.Domain.Core.ProductAgg.AppService;
using Shopify.Domain.Core.ProductAgg.Dto;

namespace Shopify.Presentation.RazorPages.Pages.Area.Admin.Pages
{
    public class ProductsModel(IProductAppService productAppService,ICategoryAppService categoryAppService) : PageModel
    {
        public ICollection<AdminProductDto> Products { get; set; }

        public SelectList Categories { get; set; }

        public async Task OnGet(CancellationToken cancellationToken)
        {
            Products = await productAppService.GetProductsForAdmin(cancellationToken);

            var cats = await categoryAppService.GetAll(cancellationToken);
            Categories = new SelectList(cats, "Id", "Name");
        }

        public async Task<IActionResult> OnPostChangeCategory(int productId, int newCategoryId, CancellationToken cancellationToken)
        {
            await productAppService.ChangeCategory(productId, newCategoryId, cancellationToken);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDelete(int id, CancellationToken cancellationToken)
        {
            await productAppService.Delete(id, cancellationToken);
            return RedirectToPage();
        }
    }
}
