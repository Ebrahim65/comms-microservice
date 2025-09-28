using EmailService.DTOs;
using EmailService.Models;
using EmailService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmailService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send")]
        public async Task<ActionResult<EmailResponse>> SendEmail([FromBody] SendEmailRequest request)
        {
            var email = new EmailMessage
            {
                To = request.To,
                From = "", // handled by EmailSettings
                Subject = request.Subject,
                Body = request.Body
            };

            var success = await _emailService.SendEmailAsync(email);

            return Ok(new EmailResponse
            {
                Success = success,
                Message = success ? "Email sent successfully" : "Failed to send email"
            });
        }
    }
}
