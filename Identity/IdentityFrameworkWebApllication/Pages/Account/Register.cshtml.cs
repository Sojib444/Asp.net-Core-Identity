using IdentityFrameworkWebApllication.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;

namespace IdentityFrameworkWebApllication.Account
{
    public class RegisterModel : PageModel
    {
        public RegisterModel(UserManager<IdentityUser> userManager,ISendEmail sendEmail)
        {
            UserManager = userManager;
            SendEmail = sendEmail;
        }

        [BindProperty]
        public Register Credintial { get; set; }
        public UserManager<IdentityUser> UserManager { get; }
        public ISendEmail SendEmail { get; }

        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            var user = new IdentityUser()
            {
                Email = Credintial.Email,
                UserName=Credintial.Email
            };
            var res =  UserManager.CreateAsync(user,Credintial.Password);

            if (res.Result.Succeeded)
            {
                var confirmationToken = await UserManager.GenerateEmailConfirmationTokenAsync(user);

                var confirmationLink = Url.PageLink("/Account/EmailConfirmation", values: new
                {
                    id = user.Id,
                    token = confirmationToken
                });

                await SendEmail.SendAsync("mdsojibhosen444@gmail.com", user.Email,
                          "Confirm Your Email", $"Please click this Link {confirmationLink}");
                return RedirectToPage("/Account/Login");

            }
            else
            {
                foreach(var item in res.Result.Errors)
                {
                    ModelState.AddModelError("Register", item.Description);
                }
            }

            return Page();
            


        }


    }

    public class Register
    {
        [Required]
        [EmailAddress(ErrorMessage ="Invalid Email Address")]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
