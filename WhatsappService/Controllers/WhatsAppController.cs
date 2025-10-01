using Microsoft.AspNetCore.Mvc;
using WhatsAppService.DTOs;
using WhatsAppService.Services.Interfaces;

namespace WhatsAppService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WhatsAppController : ControllerBase
    {
        private readonly IWhatsAppService _whatsAppService;

        public WhatsAppController(IWhatsAppService whatsAppService)
        {
            _whatsAppService = whatsAppService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] SendWhatsAppRequest request)
        {
            var result = await _whatsAppService.SendMessageAsync(request);
            return Ok(result);
        }
    }
}
