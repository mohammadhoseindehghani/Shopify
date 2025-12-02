using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopify.Domain.Core.UserAgg.AppService;

namespace Shopify.Presentation.RazorPages.Pages
{
    [BindProperties]
    public class LoginModel(IUserAppService userAppService) : PageModel
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
                ModelState.AddModelError("",result.Message!);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, result.Data!.Id.ToString()),
                new Claim(ClaimTypes.Name, result.Data.FirstName),
                new Claim(ClaimTypes.Role,result.Data.Role.ToString()),
                new Claim("Phone", result.Data.Phone)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToPage("/Index"); 
        }
    }
}
