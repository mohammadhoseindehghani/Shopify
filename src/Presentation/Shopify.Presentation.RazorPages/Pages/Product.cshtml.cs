using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopify.Domain.AppService;
using Shopify.Domain.Core.CartAgg.AppService;
using Shopify.Domain.Core.ProductAgg.AppService;
using Shopify.Domain.Core.ProductAgg.Dto;
using System.Security.Claims;

namespace Shopify.Presentation.RazorPages.Pages
{
    public class ProductModel(IProductAppService productAppService, ICartAppService cartAppService) : PageModel
    {
        public ProductDetailDto ProductDetail { get; set; }
        public async Task OnGet(int id, CancellationToken cancellationToken)
        {
            var result  = await productAppService.GetById(id, cancellationToken);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("",result.Message!);
            }

            ProductDetail = result.Data!;
        }

        public async Task<IActionResult> OnPostAddToCart([FromBody] AddToCartRequest request, CancellationToken cancellationToken)
        {
            var (userId, guestId) = GetUserOrGuestId();

            if (userId == null && guestId == 0)
            {
                guestId = new Random().Next(100000, 999999); 
                SetGuestCookie(guestId);
            }

            int cartId = 0;
            var cartResult = await cartAppService.GetCart(userId, guestId, cancellationToken);

            if (cartResult.IsSuccess && cartResult.Data != null)
            {
                cartId = cartResult.Data.Id;
            }
            else
            {
                var createResult = await cartAppService.CreateCart(userId, guestId, cancellationToken);
                if (!createResult.IsSuccess)
                {
                    return new JsonResult(new { success = false, message = "خطا در ایجاد سبد خرید" });
                }
                cartId = createResult.Data;
            }
            var currentCart = (await cartAppService.GetCart(userId, guestId, cancellationToken)).Data;
            var existingItem = currentCart?.Items.FirstOrDefault(x => x.ProductId == request.ProductId);

            if (existingItem != null)
            {
                await cartAppService.UpdateItemQuantity(cartId, request.ProductId, existingItem.Quantity + request.Quantity, cancellationToken);
            }
            else
            {
                await cartAppService.AddItemToCart(cartId, request.ProductId, request.Quantity, cancellationToken);
            }

            return new JsonResult(new { success = true, message = "محصول با موفقیت اضافه شد" });
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

        private void SetGuestCookie(int guestId)
        {
            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(30),
                HttpOnly = true,
                IsEssential = true
            };
            Response.Cookies.Append("CartGuestId", guestId.ToString(), options);
        }


        public class AddToCartRequest
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }
    }
}
