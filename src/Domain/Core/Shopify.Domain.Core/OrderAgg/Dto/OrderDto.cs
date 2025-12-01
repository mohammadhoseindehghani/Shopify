using Shopify.Domain.Core.OrderAgg.Enums;

namespace Shopify.Domain.Core.OrderAgg.Dto;

public class OrderDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserFirstName { get; set; } 
    public string UserLastName { get; set; } 
    public decimal TotalAmount { get; set; }
    public OrderStatusEnum Status { get; set; }
    public bool IsFinalized { get; set; }
    public DateTime CreatedAt { get; set; } 
    public ICollection<OrderItemDto> Items { get; set; }
}
