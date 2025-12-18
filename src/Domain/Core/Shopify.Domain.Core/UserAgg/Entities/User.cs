using Microsoft.AspNetCore.Identity;
using Shopify.Domain.Core.OrderAgg.Entities;
using Shopify.Domain.Core.UserAgg.Enums;
namespace Shopify.Domain.Core.UserAgg.Entities;

public class User : IdentityUser<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsActive { get; set; }
    public string? ImgUrl { get; set; }
    public decimal Balance { get; set; }
    //public RoleEnum Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
    public ICollection<Address> Addresses { get; set; }
    public ICollection<Order> Orders { get; set; }
}