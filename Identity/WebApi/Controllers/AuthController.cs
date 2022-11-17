using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private  IConfiguration Configuration { get; }

        [HttpPost]
        public IActionResult ChaeckAuth([FromBody] Credential credential)
        {
            if (credential.Password == "12345")
            {

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email,"mdsojibhosen44@gmail.com"),
                    new Claim(ClaimTypes.Name,"Admin"),
                    new Claim("Department","HR"),
                    new Claim("EmpolyeDate","1-1-2022")

                };

                var expairat = DateTime.UtcNow.AddMinutes(10);

                return Ok(new
                {
                    access_token = GenerateToken(claims,expairat),
                    expiryTime = expairat
                });
            }

             ModelState.AddModelError("Unauthorized", "You are not elegible for access token");
            return Unauthorized(ModelState);
        }

        private string GenerateToken(IEnumerable<Claim> claims,DateTime expiryDate)
        {

            var secretkey = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("SecretKey"));
            var jwt = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expiryDate,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secretkey),
                                            SecurityAlgorithms.HmacSha256Signature));

            return new JwtSecurityTokenHandler().WriteToken(jwt);

        }

        public class Credential
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}
