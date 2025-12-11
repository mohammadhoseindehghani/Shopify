using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopify.Domain.Core.CategoryAgg.AppService;
using Shopify.Domain.Core.CategoryAgg.Dto;

namespace Shopify.Presentation.RazorPages.Pages.Area.Admin.Pages
{
    public class CategoriesModel(ICategoryAppService categoryAppService) : PageModel
    {
        [BindProperty]
        public CreateCategoryDto CreateInput { get; set; }
        [BindProperty]
        public string NewNameCategory { get; set; }
        public ICollection<CategoryDto> Categories { get; set; }

        public int TotalCategories => Categories.Count;

        public string? ErrorMessage { get; set; }

        public async Task OnGet(CancellationToken cancellationToken)
        {
             Categories = await categoryAppService.GetAll(cancellationToken);

        }


        public async Task<IActionResult> OnPostAdd(string Name, int? ParentId, CancellationToken cancellationToken)
        {
            var dto = new CreateCategoryDto { Name = Name, ParentId = ParentId };

            var result = await categoryAppService.Add(dto, cancellationToken);

            if (!result.IsSuccess)
            {
                ErrorMessage = result.Message;
                await OnGet(cancellationToken);
                return Page();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDelete(int id, CancellationToken cancellationToken)
        {
            var result = await categoryAppService.Delete(id, cancellationToken);
            if (!result.IsSuccess)
            {
                ErrorMessage = result.Message;
                await OnGet(cancellationToken);
                return Page();

            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEdit(int id,string name, CancellationToken cancellationToken)
        {
            var result = await categoryAppService.Edit(id,name, cancellationToken);
            if (!result.IsSuccess)
            {
                ErrorMessage = result.Message;
                await OnGet(cancellationToken);
                return Page();

            }
            return RedirectToPage();
        }
    }
}
