using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace HangFireDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HangFireController : ControllerBase
    {
        [HttpGet("welcome")] // Make sure this is specified to handle the "welcome" endpoint
        public IActionResult Get()
        {
            var JobId = BackgroundJob.Enqueue(() => SendWelcomeEmail("Welcome To Our App"));
            return Ok($"JobId {JobId}: Welcome Email Sent to the user");
        }

        public void SendWelcomeEmail(string Text)
        {
            Console.WriteLine(Text);
        }
    }
}
