using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopify.Domain.Core.ProductAttributeAgg.AppService;
using Shopify.Domain.Core.ProductAttributeAgg.Dto;

namespace Shopify.Presentation.RazorPages.Pages.Area.Admin.Pages
{
    public class AttributesModel(IProductAttributeAppService attributeAppService) : PageModel
    {
        public ICollection<ProductAttributeDto> Attributes { get; set; }

    
        [BindProperty]
        public string NewAttributeName { get; set; }

        public async Task OnGet(CancellationToken cancellationToken)
        {
            Attributes = await attributeAppService.GetAll(cancellationToken);
        }

        public async Task<IActionResult> OnPostAdd(CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(NewAttributeName))
            { 
                return RedirectToPage();
            }

            await attributeAppService.Add(NewAttributeName, cancellationToken);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDelete(int id, CancellationToken cancellationToken)
        {
            await attributeAppService.Delete(id, cancellationToken);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdate(int id, string name, CancellationToken cancellationToken)
        {
            await attributeAppService.Update(id, name, cancellationToken);
            return RedirectToPage();
        }
    }
}
