using WhatsAppService.DTOs;

namespace WhatsAppService.Services.Interfaces
{
    public interface IWhatsAppService
    {
        Task<WhatsAppResponse> SendMessageAsync(SendWhatsAppRequest request);
    }
}
