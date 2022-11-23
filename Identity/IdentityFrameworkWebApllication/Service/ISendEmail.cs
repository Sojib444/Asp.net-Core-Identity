using Microsoft.Extensions.Options;

namespace IdentityFrameworkWebApllication.Service
{
    public interface ISendEmail
    {

        Task SendAsync(string from, string to, string sub, string body);
    }
}