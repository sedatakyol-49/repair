using System.ComponentModel.DataAnnotations;

namespace Repair.API.Models;

public class RepairStatus
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(20)]
    public string Status { get; set; } = string.Empty;

    public DateTime Timestamp { get; set; }

    [MaxLength(500)]
    public string? Notes { get; set; }

    public Guid RepairId { get; set; }
    public RepairModel Repair { get; set; } = null!;
}