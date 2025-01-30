using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace HangFireDemo.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var JobId = BackgroundJob.Enqueue(() => SendWelcomeEmail("Welcome To Our App"));
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        //[HttpGet(Name = "GetWeatherForecast")] 
        //public IActionResult Get()
        //{
        //    var JobId = BackgroundJob.Enqueue(() => SendWelcomeEmail("Welcome To Our App"));
        //    return Ok($"JobId {JobId}: Welcome Email Sent to the user");
        //}

        public void SendWelcomeEmail(string Text)
        {
            Console.WriteLine(Text);
        }
    }
}
