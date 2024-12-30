using System.ComponentModel.DataAnnotations;

namespace Repair.Infrastructure.Models;

public class RepairModel
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    public List<string> ReceivedImages { get; set; } = new();
    public List<string>? CompletedImages { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime EstimatedCompletionDate { get; set; }
    public DateTime AppointmentDate { get; set; }

    [Required]
    [MaxLength(10)]
    public string AppointmentTime { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Surname { get; set; } = string.Empty;

    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(20)]
    public string Phone { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Type { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Brand { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Model { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? SerialNumber { get; set; }

    public ICollection<RepairStatus> StatusHistory { get; set; } = new List<RepairStatus>();
}