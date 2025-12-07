using Shopify.Domain.Core.UserAgg.Enums;

namespace Shopify.Domain.Core.UserAgg.Dto;

public class CreateUserDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string ImgUrl { get; set; }
}