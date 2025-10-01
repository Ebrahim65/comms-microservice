namespace SmsService.Services
{
    using SmsService.Interfaces;
    using SmsService.Models;
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class AfricasTalkingSmsService : ISmsService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public AfricasTalkingSmsService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<bool> SendSmsAsync(SmsRequestViewModel request)
        {
            var apiKey = _config["AfricasTalking:ApiKey"];
            var username = _config["AfricasTalking:Username"]; // "sandbox" in dev
            var url = "https://api.sandbox.africastalking.com/version1/messaging";

            var form = new Dictionary<string, string>
    {
        { "username", username },
        { "to", request.To },
        { "message", request.Message }
    };

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(form)
            };

            // Required headers
            httpRequest.Headers.Add("apiKey", apiKey);
            httpRequest.Headers.Add("Accept", "application/json");

            var response = await _httpClient.SendAsync(httpRequest);

            // (Optional) read content for debugging
            var content = await response.Content.ReadAsStringAsync();

            return response.IsSuccessStatusCode;
        }

    }
}
