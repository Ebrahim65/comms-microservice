namespace SmsService.Models
{
    public class SmsRequestViewModel
    {
        public string To { get; set; }      // Recipient phone number
        public string Message { get; set; } // SMS body
    }
}
