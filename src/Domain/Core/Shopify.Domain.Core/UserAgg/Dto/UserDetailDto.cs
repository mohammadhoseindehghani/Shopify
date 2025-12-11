using Shopify.Domain.Core.UserAgg.Enums;

namespace Shopify.Domain.Core.UserAgg.Dto;

public class UserDetailDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime Created { get; set; }
    public string Phone { get; set; }
    public RoleEnum Role { get; set; }
    public bool IsActive { get; set; }
    public string ImgUrl { get; set; }
    public decimal Balance { get; set; }
    public int OrderCount { get; set; }
}