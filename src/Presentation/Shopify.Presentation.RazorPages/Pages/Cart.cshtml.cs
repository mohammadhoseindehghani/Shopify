using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopify.Domain.Core.CartAgg.AppService;
using Shopify.Domain.Core.CartAgg.Dto;
using System.Security.Claims;

namespace Shopify.Presentation.RazorPages.Pages
{
    public class CartModel(ICartAppService cartAppService) : PageModel
    {
      
        public CartDto Cart { get; set; } = new CartDto();
        public decimal TotalAmount => Cart.Items?.Sum(i => i.Quantity * i.UnitPrice) ?? 0; 

        public async Task OnGet(CancellationToken cancellationToken)
        {
            var (userId, guestId) = GetUserOrGuestId();
            if (userId == null && guestId == 0) return;

            var result = await cartAppService.GetCart(userId, guestId, cancellationToken);
            if (result.IsSuccess && result.Data != null)
            {
                Cart = result.Data;
            }
        }

        public async Task<IActionResult> OnPostIncrease(int productId, CancellationToken cancellationToken)
        {
            return await ChangeQuantity(productId, 1, cancellationToken);
        }

        public async Task<IActionResult> OnPostDecrease(int productId, CancellationToken cancellationToken)
        {
            return await ChangeQuantity(productId, -1, cancellationToken);
        }

        public async Task<IActionResult> OnPostRemove(int productId, CancellationToken cancellationToken)
        {
            var (userId, guestId) = GetUserOrGuestId();
            var cartResult = await cartAppService.GetCart(userId, guestId, cancellationToken);

            if (cartResult.IsSuccess && cartResult.Data != null)
            {
                await cartAppService.RemoveItemFromCart(cartResult.Data.Id, productId, cancellationToken);
            }
            return RedirectToPage();
        }

        private async Task<IActionResult> ChangeQuantity(int productId, int change, CancellationToken cancellationToken)
        {
            var (userId, guestId) = GetUserOrGuestId();
            var cartResult = await cartAppService.GetCart(userId, guestId, cancellationToken);

            if (cartResult.IsSuccess && cartResult.Data != null)
            {
                var item = cartResult.Data.Items.FirstOrDefault(i => i.ProductId == productId);
                if (item != null)
                {
                    int newQuantity = item.Quantity + change;

                    if (newQuantity <= 0)
                    {
                        await cartAppService.RemoveItemFromCart(cartResult.Data.Id, productId, cancellationToken);
                    }
                    else
                    {
                        await cartAppService.UpdateItemQuantity(cartResult.Data.Id, productId, newQuantity, cancellationToken);
                    }
                }
            }
            return RedirectToPage();
        }

        private (int? userId, int guestId) GetUserOrGuestId()
        {
            int? userId = null;
            int guestId = 0;

            if (User.Identity.IsAuthenticated)
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null) userId = int.Parse(userIdClaim.Value);
            }

            if (Request.Cookies.TryGetValue("CartGuestId", out string cookieValue))
            {
                int.TryParse(cookieValue, out guestId);
            }

            return (userId, guestId);
        }
    }
}
