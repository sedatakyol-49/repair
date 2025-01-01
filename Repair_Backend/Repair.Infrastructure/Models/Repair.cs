using System.ComponentModel.DataAnnotations;

namespace Repair.Infrastructure.Models;

public class RepairModel:BaseEntity
{
    [Required]
    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;
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