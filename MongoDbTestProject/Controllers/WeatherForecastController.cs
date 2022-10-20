using Microsoft.AspNetCore.Mvc;
using MongoDbTestProject.Services;

namespace MongoDbTestProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMongoRepository _mongoService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMongoRepository mongoService)
        {
            _logger = logger;
            _mongoService = mongoService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var result =  Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            var returnData = await _mongoService.InsertRecordAsync("test",result.FirstOrDefault());

            return result;
        }
    }
}