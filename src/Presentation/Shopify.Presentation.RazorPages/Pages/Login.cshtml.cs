using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopify.Domain.Core.CartAgg.AppService;
using Shopify.Domain.Core.UserAgg.AppService;

namespace Shopify.Presentation.RazorPages.Pages
{
    [BindProperties]
    public class LoginModel(IUserAppService userAppService, ICartAppService cartAppService) : PageModel
    {
        public string Phone { get; set; }
        public string Password { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            var result = await userAppService.Login(Phone, Password, cancellationToken);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Message!);
                return Page(); 
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, result.Data!.Id.ToString()),
                new Claim(ClaimTypes.Name, result.Data.FirstName),
                new Claim(ClaimTypes.Role, result.Data.Role.ToString()),
                new Claim("Phone", result.Data.Phone)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            int guestId = 0;
            if (Request.Cookies.TryGetValue("CartGuestId", out string? cookieValue))
            {
                int.TryParse(cookieValue, out guestId);
            }
            if (guestId != 0)
            {
                await cartAppService.MergeCarts(result.Data.Id, guestId, cancellationToken);
                Response.Cookies.Delete("CartGuestId");
            }

            return RedirectToPage("/Index");
        }
    }
}
