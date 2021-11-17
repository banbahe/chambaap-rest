using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using chambapp.dto;

namespace chambapp.bll.Helpers
{
    public class HttpHelper<T>
    {
        //   private readonly IHttpClientFactory _httpClientFactory;

        //public static async Task<T> GetAsync(string url)
        //{

        //    //Creamos una instancia de HttpClient
        //    var client = new HttpClient();

        //    //Asignamos la URL
        //    client.BaseAddress = new Uri(url);
        //    //Llamada asíncrona al sitio
        //    var response = await client.GetAsync(client.BaseAddress);
        //    //Nos aseguramos de recibir una respuesta satisfactoria

        //    response.EnsureSuccessStatusCode();
        //    //Convertimos la respuesta a una variable string
        //    var jsonResult = response.Content.ReadAsStringAsync().Result;
        //    //Se deserializa la cadena y se convierte en una instancia de WeatherResult
        //    var result = JsonConvert.DeserializeObject<T>(jsonResult);
        //    return result;
        //}

        //private static HttpClient _client = new HttpClient();


        //        private static HttpClient _client = new HttpClient();
        public static async Task<T> GetAsync(string uri)
        {
            HttpClient _client = new HttpClient();
            // _client.BaseAddress = new Uri(configGoogle.baseuri);
            // HttpContent content = new StringContent(string.Empty, System.Text.Encoding.UTF8, "application/json");
            var httpResponse = await _client.GetAsync(uri);
            if (httpResponse.IsSuccessStatusCode)
            {
                var result = await httpResponse.Content.ReadAsStringAsync();
                var postResult = JsonSerializer.Deserialize<T>(result);
                return postResult;
            }
            else
                return default(T);
        }
        public static async Task<T> GetGmpAsync(string endpoint)
        {
            HttpClient _client = new HttpClient();
            var configGoogle = _GoogleMapsSettings();

            _client.BaseAddress = new Uri(configGoogle.baseuri);
            // HttpContent content = new StringContent(string.Empty, System.Text.Encoding.UTF8, "application/json");
            endpoint = string.Concat(endpoint, configGoogle.keyparameter);
            var httpResponse = await _client.GetAsync(endpoint);
            if (httpResponse.IsSuccessStatusCode)
            {
                var result = await httpResponse.Content.ReadAsStringAsync();
                var postResult = JsonSerializer.Deserialize<T>(result, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return postResult;
            }
            else
                return default(T);
        }

        private static (string baseuri, string key, string keyparameter) _GoogleMapsSettings()
        {
            RootGMP config = new RootGMP();
            using (StreamReader r = new StreamReader("./assets/chambappsettings.json"))
            {
                string json = r.ReadToEnd();
                config = JsonSerializer.Deserialize<RootGMP>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            return (baseuri: config.GMP.BaseURI, key: config.GMP.Key, keyparameter: config.GMP.KeyParameter);
        }

    }
}
