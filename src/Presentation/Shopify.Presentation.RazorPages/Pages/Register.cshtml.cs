using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopify.Domain.Core.UserAgg.AppService;
using Shopify.Domain.Core.UserAgg.Dto;
using System.ComponentModel.DataAnnotations;

namespace Shopify.Presentation.RazorPages.Pages
{
    [BindProperties]
    public class RegisterModel(IUserAppService userAppService, ILogger<RegisterModel> logger) : PageModel
    {
        [Required(ErrorMessage = "نام اجباری است")]
        [MaxLength(30, ErrorMessage = "نام حداکثر باید 30 کاراکتر باشد")]
        [Display(Name = "نام")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "نام خانوادگی اجباری است")]
        [MaxLength(30, ErrorMessage = "نام خانوادگی حداکثر باید 30 کاراکتر باشد")]
        [Display(Name = "نام خانوادگی")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "تلفن اجباری است")]
        [Phone(ErrorMessage = "فرمت اشتباه")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "تلفن باید 11 کاراکتر باشد")]
        [Display(Name = "تلفن همراه")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "این فیلد احباری است")]
        [MinLength(6, ErrorMessage = "حداقل باید 6 کاراکتر باشه")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [Required(ErrorMessage = "این فیلد احباری است")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "تکرارش یکی نیس!")]
        [Display(Name = "تکرار رمز عبور")]
        public string RePassword { get; set; }


        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("خطا در ثبت نام کاربر");
                return Page();
            }

            var userDto = new RegisterUserDto
            {
                FirstName = Firstname,
                LastName = Lastname,
                Phone = Phone,
                Password = Password
            };

            var result = await userAppService.Register(userDto, cancellationToken);

            if (!result.IsSuccess)
            {
                logger.LogWarning($"کاربر {Phone} به دلیلی {result.Message} ثبت نام نشد");

                ModelState.AddModelError(string.Empty, result.Message);
                return Page();
            }

            logger.LogInformation($"کاربر {Phone} با موفقیت رجیستر شد");

            return RedirectToPage("/Login");
        }
    }
}
