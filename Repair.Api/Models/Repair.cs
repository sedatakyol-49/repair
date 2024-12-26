using System.ComponentModel.DataAnnotations;

namespace Repair.API.Models;

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

    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    public Product Product { get; set; } = null!;
    public ICollection<RepairStatus> StatusHistory { get; set; } = new List<RepairStatus>();
}