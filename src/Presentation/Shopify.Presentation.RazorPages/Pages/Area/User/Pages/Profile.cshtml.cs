using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopify.Domain.AppService;
using Shopify.Domain.Core.UserAgg.AppService;
using Shopify.Domain.Core.UserAgg.Dto;
using System.Security.Claims;

namespace Shopify.Presentation.RazorPages.Pages.Area.User.Pages
{
    public class ProfileModel(IUserAppService userAppService): PageModel
    {
        public UserDto CurrentUser { get; set; }

        [BindProperty]
        public EditUserDto EditProfileInput { get; set; }

        [BindProperty]
        public ChangePasswordDto ChangePasswordInput { get; set; }

        [TempData]
        public string StatusMessage { get; set; }
        [TempData]
        public string StatusType { get; set; } 

        public async Task<IActionResult> OnGet(CancellationToken cancellationToken)
        {
            var userId = GetCurrentUserId();
            var result = await userAppService.GetById(userId, cancellationToken);

            if (!result.IsSuccess || result.Data == null)
            {
                return RedirectToPage("/Login");
            }

            CurrentUser = result.Data;

            EditProfileInput = new EditUserDto
            {
                FirstName = CurrentUser.FirstName,
                LastName = CurrentUser.LastName,
                Email = CurrentUser.Email,
                PhoneNumber = CurrentUser.Phone,
                ImgUrl = CurrentUser.ImgUrl
            };

            return Page();
        }

        public async Task<IActionResult> OnPostUpdateProfile(CancellationToken cancellationToken)
        {
            var userId = GetCurrentUserId();


            var result = await userAppService.Edit(userId, EditProfileInput, cancellationToken);

            if (result.IsSuccess)
            {
                StatusMessage = "اطلاعات پروفایل با موفقیت بروزرسانی شد.";
                StatusType = "success";
            }
            else
            {
                StatusMessage = result.Message;
                StatusType = "danger";
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostChangePassword(CancellationToken cancellationToken)
        {
            var userId = GetCurrentUserId();

            if (ChangePasswordInput.NewPassword != ChangePasswordInput.ConfirmNewPassword)
            {
                StatusMessage = "تکرار رمز عبور جدید مطابقت ندارد.";
                StatusType = "danger";
                return RedirectToPage();
            }

            var result = await userAppService.ChangePassword(userId, ChangePasswordInput, cancellationToken);

            if (result.IsSuccess)
            {
                StatusMessage = "رمز عبور با موفقیت تغییر کرد.";
                StatusType = "success";
            }
            else
            {
                StatusMessage = result.Message;
                StatusType = "danger";
            }

            return RedirectToPage();
        }

        private int GetCurrentUserId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
