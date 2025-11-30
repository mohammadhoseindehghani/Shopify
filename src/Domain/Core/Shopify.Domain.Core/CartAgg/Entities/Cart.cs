using Shopify.Domain.Core._common;
using Shopify.Domain.Core.UserAgg.Entities;

namespace Shopify.Domain.Core.CartAgg.Entities;

public class Cart : BaseEntity
{
    public int? UserId { get; set; }
    public User User { get; set; }

    public int? GuestId { get; set; }
    public ICollection<CartItem> Items { get; set; }
}