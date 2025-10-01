using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using WhatsAppService.Config;
using WhatsAppService.DTOs;
using WhatsAppService.Services.Interfaces;

namespace WhatsAppService.Services.Implementations
{
    public class MetaWhatsAppService : IWhatsAppService
    {
        private readonly HttpClient _httpClient;
        private readonly WhatsAppSettings _settings;

        public MetaWhatsAppService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _settings = config.GetSection("WhatsApp").Get<WhatsAppSettings>();
        }

        public async Task<WhatsAppResponse> SendMessageAsync(SendWhatsAppRequest request)
        {
            var url = $"https://graph.facebook.com/v22.0/{_settings.PhoneNumberId}/messages";

            var payload = new
            {
                messaging_product = "whatsapp",
                to = request.To,
                type = "template",
                template = new
                {
                    name = "hello_world",
                    language = new { code = "en_US" }
                }
            };


            var json = JsonSerializer.Serialize(payload);
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            httpRequest.Headers.Add("Authorization", $"Bearer {_settings.AccessToken}");

            var response = await _httpClient.SendAsync(httpRequest);
            var content = await response.Content.ReadAsStringAsync();

            return new WhatsAppResponse
            {
                Success = response.IsSuccessStatusCode,
                ResponseMessage = content,
                MessageId = response.IsSuccessStatusCode ? Guid.NewGuid().ToString() : null
            };
        }
    }
}
