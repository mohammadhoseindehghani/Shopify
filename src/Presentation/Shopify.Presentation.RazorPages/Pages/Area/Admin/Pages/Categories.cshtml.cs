using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopify.Domain.Core.CategoryAgg.AppService;
using Shopify.Domain.Core.CategoryAgg.Dto;

namespace Shopify.Presentation.RazorPages.Pages.Area.Admin.Pages
{
    public class CategoriesModel(ICategoryAppService categoryAppService) : PageModel
    {
        public ICollection<CategoryDto> Categories { get; set; }
        public async Task OnGet(CancellationToken cancellationToken)
        {
            Categories = await categoryAppService.GetAll(cancellationToken);
        }
    }
}
