using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopify.Domain.Core.OrderAgg.AppService;
using Shopify.Domain.Core.ProductAgg.AppService;
using Shopify.Domain.Core.UserAgg.AppService;

namespace Shopify.Presentation.RazorPages.Pages.Area.Admin.Pages
{
    [Authorize(Roles = "Admin")]
    public class DashboardModel(
        IOrderAppService orderAppService, 
        IProductAppService productAppService, 
        IUserAppService userAppService) : PageModel
    {
        public decimal TotalOrderMoney { get; set; }
        public int TotalUsers { get; set; }
        public int TotalOrders { get; set; }
        public int TotalProducts { get; set; }
        public int ProductsOutOfStock { get; set; }
        public int ProductsRunningLow { get; set; }
        public int ProductsInStock { get; set; }

        public async Task OnGet(CancellationToken ct)
        {
            TotalOrders = await orderAppService.OrderCount(ct);
            TotalOrderMoney = await orderAppService.TotalOrder(ct);
            TotalUsers = await userAppService.UserCount(ct);
            TotalProducts = await productAppService.ProductCount(ct);
            ProductsOutOfStock = await productAppService.GetProductsOutOfStock(ct);
            ProductsRunningLow = await productAppService.GetProductsRunningLow(ct);
            ProductsInStock = await productAppService.GetProductsInStock(ct);
        }
    }
}
