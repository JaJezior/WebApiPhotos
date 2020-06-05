using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PictureWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<PictureDTO> pictures = new List<PictureDTO>();

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<PictureDTO> Get(string id)
        {
            var selectedPicture = pictures.FirstOrDefault(p => p.Id == id);
            return selectedPicture;
        }
        [HttpPost]
        public void Post([FromBody] PictureDTO value)
        {
            if (pictures.Any(p => p.Id == value.Id))
            {
                pictures.Remove(pictures.First(p => p.Id == value.Id));
            }
            pictures.Add(value);
        }

    }
    public class PictureDTO
    {
        public string Id { get; set; }
        public string Content { get; set; }
    }
}
