namespace Shopify.Domain.Core.UserAgg.Dto;

public class UserWithPasswordDto : UserDto
{
    public string PasswordHash { get; set; }
}