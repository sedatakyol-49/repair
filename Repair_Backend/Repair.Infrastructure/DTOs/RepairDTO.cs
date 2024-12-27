using Repair.Infrastructure.Models;

namespace Repair.Infrastructure.DTOs;

public class RepairDTO {
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public List<string>? ReceivedImages { get; set; }
    public List<string>? CompletedImages { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? EstimatedCompletionDate { get; set; }
    public DateTime? AppointmentDate { get; set; }
    public string? AppointmentTime { get; set; }
    public Customer Customer { get; set; } = new Customer();
    public Product Product { get; set; } = new Product();
    public List<RepairStatus>? StatusHistory { get; set; }

}

