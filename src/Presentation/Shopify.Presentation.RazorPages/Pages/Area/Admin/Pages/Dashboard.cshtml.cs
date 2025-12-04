using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shopify.Presentation.RazorPages.Pages.Area.Admin.Pages
{
    [Authorize(Roles = "Admin")]
    public class DashboardModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
