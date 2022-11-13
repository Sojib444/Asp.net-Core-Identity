using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Identity.Pages
{
    public class SignOutModel : PageModel
    {
        public IActionResult OnPost()
        {
            HttpContext.SignOutAsync("MyCookie");
            return Redirect("/Account/Login");
        }
    }
}
