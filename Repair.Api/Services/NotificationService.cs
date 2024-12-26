using Repair.API.Repositories;

namespace Repair.API.Services;

public class NotificationService : INotificationService
{
    private readonly IRepairRepository _repairRepository;

    public NotificationService(IRepairRepository repairRepository)
    {
        _repairRepository = repairRepository;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        // Implement email sending logic (e.g., using SendGrid, Amazon SES, etc.)
        await Task.CompletedTask;
    }

    public async Task SendSMSAsync(string to, string message)
    {
        // Implement SMS sending logic (e.g., using Twilio, etc.)
        await Task.CompletedTask;
    }

    public async Task NotifyCustomerAsync(Guid repairId, string newStatus)
    {
        var repair = await _repairRepository.GetRepairWithDetailsAsync(repairId);
        if (repair == null) return;

        var statusText = GetStatusText(newStatus);

        var emailSubject = $"Repair Status Update - {statusText}";
        var emailBody = $@"
Dear {repair.Customer.Name},

Your repair order for {repair.Product.Brand} {repair.Product.Model} has been updated.
Current Status: {statusText}

Track your repair at: [Repair Tracking URL]

Best regards,
Repair Service Team";

        var smsMessage = $"Repair Update: Your {repair.Product.Brand} {repair.Product.Model} repair status is now \"{statusText}\". Track at: [URL]";

        await SendEmailAsync(repair.Customer.Email, emailSubject, emailBody);
        await SendSMSAsync(repair.Customer.Phone, smsMessage);
    }

    private string GetStatusText(string status) => status switch
    {
        "received" => "Device Received",
        "diagnosing" => "Under Diagnosis",
        "repairing" => "Under Repair",
        "completed" => "Repair Completed",
        "delivered" => "Device Delivered",
        _ => status
    };
}