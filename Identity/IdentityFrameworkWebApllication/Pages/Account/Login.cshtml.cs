using IdentityFrameworkWebApllication.Pages.Account.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityFrameworkWebApllication.Pages.Account
{
    public class LoginModel : PageModel
    {
        public LoginModel(SignInManager<IdentityUser> user)
        {
            User = user;
        }
        [BindProperty]
        public LoginCredential? Credential { get; set; }
        public SignInManager<IdentityUser> User { get; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
           var res= await User.PasswordSignInAsync(Credential.Email, Credential.Password,
                                                    Credential.Rememberme,false);

            if(res.Succeeded)
            {
                return RedirectToPage("/Index");
            }
            else
            {
                if (res.IsLockedOut)
                {
                    ModelState.AddModelError("Login", "You are locked out.");
                }
                else
                {
                    ModelState.AddModelError("Login", "Failed to login.");
                }

                return Page();
                
            }

        }
 
    }
}
