using Shopify.Domain.Core._common;

namespace Shopify.Domain.Core.UserAgg.Entities;

public class Address : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; }
    public string Name { get; set; }
    public string Province { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string Plaque { get; set; }      
    public string UnitNumber { get; set; }  
    public string PostalCode { get; set; }
    public bool IsDefault { get; set; }
}
