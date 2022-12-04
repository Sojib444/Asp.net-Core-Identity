using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityFrameworkWebApllication.Pages.Account
{
    public class SignOutModel : PageModel
    {
        public SignOutModel(SignInManager<IdentityUser> signInManager)
        {
            SignInManager = signInManager;
        }

        public SignInManager<IdentityUser> SignInManager { get; }

        public async Task<IActionResult> OnGet()
        {

            await SignInManager.SignOutAsync();

            return RedirectToPage("/Account/Login");
        }
    }
}
