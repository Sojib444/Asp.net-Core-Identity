using System.ComponentModel.DataAnnotations;

namespace IdentityFrameworkWebApllication.Pages.Account.Model
{
    public class LoginCredential
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string ? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string  ?Password { get; set; }
        [Display(Name ="Remember Me")]
        public bool  Rememberme { get; set; }
    }
}
