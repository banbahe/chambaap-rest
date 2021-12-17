using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace chambapp.dto
{
    public class WeatherForecastDto
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
}
