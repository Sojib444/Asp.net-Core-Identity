using Identity.Authorization;
using Identity.DTO;
using Identity.Pages.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Identity.Pages
{
    [Authorize(Policy = "HRdetection")]
    public class HRModel : PageModel
    {
        private readonly IHttpClientFactory httpClientFactory;

        [BindProperty]
        public List<WeatherForecastDTO>? List { get; set; }

        public HRModel(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        private async Task<T> InvokeEndPoint<T>(string clientName, string url)
        {
            // get token from session
            JwtToken token = null;

            var strTokenObj = HttpContext.Session.GetString("access_token");
            if (string.IsNullOrWhiteSpace(strTokenObj))
                token = await Authenticate();
            else
                token = JsonConvert.DeserializeObject<JwtToken>(strTokenObj);

            if (token == null ||
                string.IsNullOrWhiteSpace(token.AccessToken) ||
                token.ExpirayAt <= DateTime.UtcNow)
                token = await Authenticate();

            var httpClient = httpClientFactory.CreateClient(clientName);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            return await httpClient.GetFromJsonAsync<T>(url);
        }

        private async Task<JwtToken> Authenticate()
        {
            var httpClient = httpClientFactory.CreateClient("OurWebAPI");
            var res = await httpClient.PostAsJsonAsync("auth", new Login { Email = "mdsojibhosen444@gmail.com", Password = "12345" });
            res.EnsureSuccessStatusCode();
            string strJwt = await res.Content.ReadAsStringAsync();
            HttpContext.Session.SetString("access_token", strJwt);

            return JsonConvert.DeserializeObject<JwtToken>(strJwt);
        }

        public async Task OnGetAsync()
        {
            List = await InvokeEndPoint<List<WeatherForecastDTO>>("OurWebAPI", "WeatherForecast");
        }

        //public async Task OnGetAsync()
        //{
        //    //Athentication and getting the token
        //    var httpClient = httpClientFactory.CreateClient("OurWebAPI");
        //    var res = await httpClient.PostAsJsonAsync("Auth", new Login { Email="mdsojibhosen444@gmail.com",Password = "12345" });
        //    res.EnsureSuccessStatusCode();
        //    string content = await res.Content.ReadAsStringAsync();
        //    var token = JsonConvert.DeserializeObject<JwtToken>(content);

        //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);


        //    List = await httpClient.GetFromJsonAsync<List<WeatherForecastDTO>>("WeatherForecast");
        //}
    }
}
