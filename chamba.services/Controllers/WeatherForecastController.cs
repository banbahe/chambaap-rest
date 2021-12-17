using chambapp.dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace chamba.services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(string),StatusCodes.Status500InternalServerError)]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get data from weather
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///  Get /Get
        ///  {
        ///      "datums":"data",
        ///      "flag":1,
        ///      "message":"test"
        ///  }
        /// </remarks>
        /// <returns>
        /// Dummy data
        /// </returns>
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        /// <summary>
        /// Get data from weather
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///  Post /Insert
        ///  {
        ///      "Date":"2021-12-11",
        ///      "TemperatureC": "100 ox",
        ///      "Summary":"test"
        ///  }
        /// </remarks>
        /// <returns>
        /// Dummy data
        /// </returns>
        [HttpPost]
        public IEnumerable<WeatherForecastDto> Insert([FromBody] WeatherForecastDto value)
        {
            return null; 
        }
    }
}
