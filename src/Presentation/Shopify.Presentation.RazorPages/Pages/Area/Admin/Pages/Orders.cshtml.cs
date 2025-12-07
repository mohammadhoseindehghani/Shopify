using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopify.Domain.Core.OrderAgg.AppService;
using Shopify.Domain.Core.OrderAgg.Dto;

namespace Shopify.Presentation.RazorPages.Pages.Area.Admin.Pages
{
    public class OrdersModel(IOrderAppService orderAppService) : PageModel
    {
        public ICollection<OrderDto> Orders { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; }



        public async Task OnGet(CancellationToken cancellationToken)
        {
            Orders = await orderAppService.GetAll(cancellationToken);
        }

        public async Task<IActionResult> OnGetOrderItems(int orderId, CancellationToken cancellationToken)
        {
            var items = await orderAppService.GetOrderItems(orderId, cancellationToken);
            return new JsonResult(items);
        }
    }
}
