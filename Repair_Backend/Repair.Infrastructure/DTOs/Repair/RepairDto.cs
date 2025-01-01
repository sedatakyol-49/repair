using Repair.Infrastructure.Models;

namespace Repair.Infrastructure.DTOs.Repair;

public class RepairDto
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? EstimatedCompletionDate { get; set; }
    public DateTime? AppointmentDate { get; set; }
    public string? AppointmentTime { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string? SerialNumber { get; set; }
    public List<RepairStatus>? StatusHistory { get; set; }

}

