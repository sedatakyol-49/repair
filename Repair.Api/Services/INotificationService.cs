namespace Repair.API.Services;

public interface INotificationService
{
    Task SendEmailAsync(string to, string subject, string body);
    Task SendSMSAsync(string to, string message);
    Task NotifyCustomerAsync(Guid repairId, string newStatus);
}