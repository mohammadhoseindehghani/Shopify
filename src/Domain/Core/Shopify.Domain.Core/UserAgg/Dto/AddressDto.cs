namespace Shopify.Domain.Core.UserAgg.Dto;

public class AddressDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Province { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public int Plaque { get; set; }
    public int UnitNumber { get; set; }
    public string PostalCode { get; set; }
    public bool IsDefault { get; set; }
}