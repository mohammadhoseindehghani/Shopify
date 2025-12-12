using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shopify.Domain.Core.CategoryAgg.AppService;
using Shopify.Domain.Core.ProductAgg.AppService;
using Shopify.Domain.Core.ProductAgg.Dto;
using Shopify.Presentation.RazorPages.Services.File;

namespace Shopify.Presentation.RazorPages.Pages.Area.Admin.Pages
{
    public class ProductsModel(IProductAppService productAppService, ICategoryAppService categoryAppService, IFileService fileService) : PageModel
    {
        public string? ErrorMessage { get; set; }
        public ICollection<AdminProductDto> Products { get; set; }
        public SelectList Categories { get; set; }

        [BindProperty]
        public EditProductDto EditProductInput { get; set; }

        [BindProperty]
        public IFormFile? NewImageFile { get; set; }

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

        public async Task<IActionResult> OnPostEdit(int id, CancellationToken cancellationToken)
        {
            var result = await productAppService.EditProduct(id, EditProductInput, cancellationToken);

            if (!result.IsSuccess)
            {
                ErrorMessage = result.Message;
                await OnGet(cancellationToken); 
                return Page();
            }
            return RedirectToPage();
        }


        public async Task<IActionResult> OnPostEditImg(int id, CancellationToken cancellationToken)
        {
            if (NewImageFile == null || NewImageFile.Length == 0)
            {
                ErrorMessage = "لطفاً یک فایل تصویر انتخاب کنید.";
                await OnGet(cancellationToken);
                return Page();
            }

            var imgUrl = await fileService.Upload(NewImageFile, "products", cancellationToken);

            var result = await productAppService.EditImg(id, imgUrl, cancellationToken);

            if (!result.IsSuccess)
            {
                await fileService.Delete(imgUrl, cancellationToken);

                ErrorMessage = result.Message;
                await OnGet(cancellationToken);
                return Page();
            }

            return RedirectToPage();
        }
    }
}
