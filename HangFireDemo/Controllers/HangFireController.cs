using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace HangFireDemo.Controllers
{
    [ApiController]
    [Route("api/hangfire")]
    [Route("hangfire")]  // Defines base route as /hangfire
    public class HangFireController : ControllerBase
    {
        [HttpGet("welcome")]  
        public IActionResult welcome()
        {
            var jobId = BackgroundJob.Enqueue(() => SendWelcomeEmail("Welcome To Our App"));
            return Ok($"JobId {jobId}: Welcome Email Sent to the user");
        }
        [HttpGet("Discount")]
        public IActionResult Discount()
        {
            int timeInSeconds = 30;
            var jobId = BackgroundJob.Schedule(() => SendWelcomeEmail("Welcome to our app"), TimeSpan.FromSeconds(timeInSeconds));
            return Ok($"Job ID: {jobId}. Discount email will be sent in 30 seconds!");
        }
        [HttpGet("DatabaseUpdate")]
        public IActionResult DatabaseUpdate()
        {
            RecurringJob.AddOrUpdate(() => Console.WriteLine("Database updated"), Cron.Minutely);
            return Ok("Database check job initiated!");
        }
        [HttpGet("Confirm")]
        public IActionResult Confirm()
        {
            int timeInSeconds = 5;
            var ParentjobId = BackgroundJob.Schedule(() => SendWelcomeEmail("You Asked to Unsusbribe"), TimeSpan.FromSeconds(timeInSeconds));
            BackgroundJob.ContinueJobWith(ParentjobId, () => SendWelcomeEmail("You are unsusbribed now !"));
            return Ok("Database check job initiated!");
        }

        public void SendWelcomeEmail(string text)
        {
            Console.WriteLine(text);
        }
    }
}
