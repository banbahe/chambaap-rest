using System.Threading.Tasks;
using chambapp.dto;
using System.Web;
using chambapp.bll.Helpers;

namespace chambapp.bll.Services
{
    public class GoogleMaps: IGoogleMaps
    {
        public async Task<RootGoogleMapsDto> TextSearchAsync(string query)
        {
            var resultTextSearch = await HttpHelper<RootGoogleMapsDto>.GetGmpAsync($"place/textsearch/json?query={query}");
            return resultTextSearch;
        }
    }
}