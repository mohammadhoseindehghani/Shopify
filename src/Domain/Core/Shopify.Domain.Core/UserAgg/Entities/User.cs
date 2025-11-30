using Shopify.Domain.Core._common;
using Shopify.Domain.Core.OrderAgg.Entities;

namespace Shopify.Domain.Core.UserAgg.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string ImgUrl { get; set; }
    public decimal Balance { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}