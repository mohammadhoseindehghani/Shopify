namespace Shopify.Domain.Core.UserAgg.Dto;

public class AdminChangePasswordDto
{
    public int UserId { get; set; }
    public string NewPassword { get; set; } 
}