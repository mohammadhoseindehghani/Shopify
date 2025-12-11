using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopify.Domain.Core.UserAgg.AppService;
using Shopify.Domain.Core.UserAgg.Dto;

namespace Shopify.Presentation.RazorPages.Pages.Area.Admin.Pages
{
    public class UserDetailsModel(IUserAppService userAppService) : PageModel
    {
        public UserDetailDto UserDetails { get; set; }

        public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
        {
            var result = await userAppService.GetUserDetail(id, cancellationToken);

            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Message;
                return RedirectToPage("Index");
            }

            UserDetails = result.Data;
            return Page(); 
        }
    }
}
