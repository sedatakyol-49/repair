using System.ComponentModel.DataAnnotations;

namespace Repair.Infrastructure.Models;
public class RepairStatus : BaseEntity
{
    [Required]
    [MaxLength(20)]
    public string Status { get; set; } = string.Empty;

    public DateTime Timestamp { get; set; }

    [MaxLength(500)]
    public string? Notes { get; set; }

    public Guid RepairId { get; set; }
    public RepairModel Repair { get; set; } = null!;
}