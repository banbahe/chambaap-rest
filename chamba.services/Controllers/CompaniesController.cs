using chambapp.dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace chambapp.services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private IHttpClientFactory _httpClientFactory;

        public CompaniesController(IHttpClientFactory httpClientFactory) 
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET address
        [HttpGet]
        [Route("ubication")]
        public async Task<ResponseModel> Ubication([FromServices] IConfiguration config,[FromQuery] string name)
        {
            var myValue = config.GetValue<string>("GMPKey");

            //// Read File
            //var client = _httpClientFactory.CreateClient("GMP");
            //// var result = await client.GetAsync("api/perfilpersonal/ivanabad");
            //// place/textsearch/json?query=morganna games&key=AIzaSyBXDZsI-p6gyfuEdFlKipbzzNvgB00TUxg
            //var result = await client.GetAsync($"place/textsearch/json?query={name}&key=")
            //Console.WriteLine(result.StatusCode);

            return null;
        }
    }
}
