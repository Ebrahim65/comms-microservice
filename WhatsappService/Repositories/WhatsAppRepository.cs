namespace WhatsAppService.Repositories
{
    public class WhatsAppRepository : IWhatsAppRepository
    {
        public Task LogMessageAsync(string to, string message, bool success)
        {
            // TODO: Implement DB persistence later
            return Task.CompletedTask;
        }
    }
}
