namespace SmsService.Interfaces
{
    using SmsService.Models;
    using System.Threading.Tasks;

    public interface ISmsService
    {
        Task<bool> SendSmsAsync(SmsRequestViewModel request);
    }
}
