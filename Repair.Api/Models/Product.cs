using System.ComponentModel.DataAnnotations;

namespace Repair.API.Models;

public class Product
{
    public Guid Id { get; set; }

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

    public Guid RepairId { get; set; }
    public RepairModel Repair { get; set; } = null!;
}