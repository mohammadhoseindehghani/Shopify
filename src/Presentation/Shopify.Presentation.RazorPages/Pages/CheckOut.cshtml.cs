using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopify.Domain.AppService;
using Shopify.Domain.Core.CartAgg.Dto;
using System.Security.Claims;
using Shopify.Domain.Core.CartAgg.AppService;
using Shopify.Domain.Core.OrderAgg.AppService;
using Shopify.Domain.Core.UserAgg.AppService;

namespace Shopify.Presentation.RazorPages.Pages
{
    public class CheckOutModel(IUserAppService userAppService,IOrderAppService orderAppService, ICartAppService cartAppService) : PageModel
    {
        public CartDto Cart { get; set; }
        public decimal UserBalance { get; set; }
        public decimal PayableAmount { get; set; }
        public decimal RemainingBalance => UserBalance - PayableAmount;
        public bool HasSufficientBalance => RemainingBalance >= 0;


        public string UserFullName { get; set; }

        public async Task<IActionResult> OnGet(CancellationToken cancellationToken)
        {
 
            if (!User.Identity.IsAuthenticated) return RedirectToPage("/Login");

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var cartResult = await cartAppService.GetUserCart(userId, cancellationToken);
            if (!cartResult.IsSuccess || cartResult.Data == null || !cartResult.Data.Items.Any())
            {
                return RedirectToPage("/Cart"); 
            }
            Cart = cartResult.Data;
            PayableAmount = Cart.Items.Sum(x => x.Quantity * x.UnitPrice);

            var userResult = await userAppService.GetById(userId, cancellationToken);
            if (userResult.IsSuccess)
            {
                UserBalance = userResult.Data.Balance;
                UserFullName = $"{userResult.Data.FirstName} {userResult.Data.LastName}";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostPay(CancellationToken cancellationToken)
        {
            if (!User.Identity.IsAuthenticated) return RedirectToPage("/Login");

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = await orderAppService.FinalizeOrder(userId, cancellationToken);

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = "سفارش شما با موفقیت ثبت شد.";
                return RedirectToPage("/Profile/Orders");
            }
            else
            {
                ModelState.AddModelError("", result.Message);
                await OnGet(cancellationToken);
                return Page();
            }
        }
    }
}
