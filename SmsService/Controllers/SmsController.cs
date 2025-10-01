namespace SmsService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SmsService.Interfaces;
    using SmsService.Models;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class SmsController : ControllerBase
    {
        private readonly ISmsService _smsService;

        public SmsController(ISmsService smsService)
        {
            _smsService = smsService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendSms([FromBody] SmsRequestViewModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _smsService.SendSmsAsync(request);

            if (result)
                return Ok(new { message = "SMS sent successfully!" });

            return StatusCode(500, "Failed to send SMS.");
        }
    }
}
