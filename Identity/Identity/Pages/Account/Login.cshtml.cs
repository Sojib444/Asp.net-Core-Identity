using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Identity.Pages.Account
{
    public class LoginModel : PageModel
    {

        [BindProperty]
        public Login Credential { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult>  OnPostAsync()
        {

            //verify the credential

            if (Credential.Password == "12345")
            {
                //**creating the security Context**//

                //Claims

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email,"mdsojibhosen44@gmail.com"),
                    new Claim(ClaimTypes.Name,"Admin"),
                    new Claim("Department","HR"),
                    new Claim("EmpolyeDate","1-1-2022")

                };

                //sent claims into Indeti

                var itendeity = new ClaimsIdentity(claims, "MyCookie");

                var principal=new ClaimsPrincipal(itendeity);

                //**security Context is generated and it can be ready to go into Cookie so 
                //so we need to encrypt and serialize this cookie.

                //set persistace cookie;
                var aithPeersistanceCookie = new AuthenticationProperties();
                aithPeersistanceCookie.IsPersistent = Credential.RememberMe;

               await HttpContext.SignInAsync("MyCookie", principal,aithPeersistanceCookie);



                return RedirectToPage("/index");
            }
            else return Page();

            
            





            


        }
    }
}
