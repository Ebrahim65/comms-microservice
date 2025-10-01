namespace WhatsAppService.Repositories
{
    public interface IWhatsAppRepository
    {
        Task LogMessageAsync(string to, string message, bool success);
    }
}
