using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopify.Domain.AppService;
using Shopify.Domain.Core.UserAgg.AppService;
using Shopify.Domain.Core.UserAgg.Dto;

namespace Shopify.Presentation.RazorPages.Pages.Area.Admin.Pages
{
    public class UsersModel(IUserAppService userAppService) : PageModel
    {
        public ICollection<UserDto> Users { get; set; }
        [BindProperty]
        public CreateUserDto NewUser { get; set; }

        public UserDetailDto UserDetails { get; set; }
        public async Task OnGet(CancellationToken cancellationToken)
        {
            Users = await userAppService.GetAll(cancellationToken);
        }

        public async Task<IActionResult> OnPostChargeWallet(int userId, decimal amount, CancellationToken cancellationToken)
        {
            await userAppService.ChargeWallet(userId, amount, cancellationToken);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostToggleStatus(int userId, bool isActive, CancellationToken cancellationToken)
        {
            if (isActive)
            {
                await userAppService.DeActive(userId, cancellationToken);
            }
            else
            {
                await userAppService.Active(userId, cancellationToken);
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAddUser(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                Users = await userAppService.GetAll(cancellationToken);
                return Page();
            }
            var result = await userAppService.Add(NewUser, cancellationToken);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Message!);
                Users = await userAppService.GetAll(cancellationToken);
                return Page();
            }
            return RedirectToPage();
        }

    }
}
