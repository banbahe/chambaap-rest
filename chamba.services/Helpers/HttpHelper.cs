using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace chambapp.services.Helpers
{
    public class HttpHelper
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpHelper(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;


        public async Task GetAsync(string url)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:44363/");
            var result = await client.GetAsync("api/perfilpersonal/ivanabad");
            Console.WriteLine(result.StatusCode);
        }
    }
}
