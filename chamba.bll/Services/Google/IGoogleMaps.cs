using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using chambapp.dto;
namespace chambapp.bll.Services
{
    public interface IGoogleMaps
    {
        Task<RootGoogleMapsDto> TextSearchAsync(string query);
    }
}
