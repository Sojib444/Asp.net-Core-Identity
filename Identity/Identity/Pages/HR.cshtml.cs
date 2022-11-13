using Identity.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;

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

        public async Task OnGetAsync()
        {
            var httpClient = httpClientFactory.CreateClient("OurWebAPI");
            List = await httpClient.GetFromJsonAsync<List<WeatherForecastDTO>>("WeatherForecast/Get");
        }
    }
}
