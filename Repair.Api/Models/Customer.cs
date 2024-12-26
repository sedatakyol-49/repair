using System.ComponentModel.DataAnnotations;

namespace Repair.API.Models;

public class Customer
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Phone { get; set; } = string.Empty;

    public ICollection<RepairModel> Repairs { get; set; } = new List<RepairModel>();
}