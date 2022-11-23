using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.Metrics;

namespace IdentityFrameworkWebApllication.Pages.Account
{
    public class EmailConfirmationModel : PageModel
    {
        public EmailConfirmationModel(UserManager<IdentityUser> user)
        {
            User = user;
        }
        [BindProperty]
        public string ?Message { get; set; }
        public UserManager<IdentityUser> User { get; }

        public async Task<IActionResult> OnGet( string id,string token)
        {
            var user=await User.FindByIdAsync(id);
            if(user!=null)
            {
                var result=await User.ConfirmEmailAsync(user, token);
                if(result.Succeeded)
                {
                    Message = "Successfully Email Varification.You can Login";
                    return Page();
                }
                else
                {
                    Message = "Can't Verify the Email";
                    return Page();
                }
            }
            Message = "User doesn't exis";
            return Page();
        }
    }
}
