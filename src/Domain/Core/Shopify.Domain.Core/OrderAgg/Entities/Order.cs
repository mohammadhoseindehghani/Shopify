using Shopify.Domain.Core._common;
using Shopify.Domain.Core.OrderAgg.Enums;
using Shopify.Domain.Core.UserAgg.Entities;

namespace Shopify.Domain.Core.OrderAgg.Entities;

public class Order : BaseEntity
{
    public int UserId { get; set; } 
    public User User { get; set; }

    public decimal TotalAmount { get; set; } 

    public OrderStatusEnum Status { get; set; } 

    public ICollection<OrderItem> OrderItems { get; set; }
}