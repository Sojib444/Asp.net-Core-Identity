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
        public IHttpClientFactory Factory { get; set; }
        public HRModel(IHttpClientFactory factory)
        {
            Factory = factory;
        }

        [BindProperty]
        public List<WeatherForecastDTO>? List { get; set; }



        public async Task OnGet()
        {
            var client=Factory.CreateClient("MywebAPI");
            List = await client.GetFromJsonAsync<List<WeatherForecastDTO>>("WeatherForecast");
        }
    }
}
