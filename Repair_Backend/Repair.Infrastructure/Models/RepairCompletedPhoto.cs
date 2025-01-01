
using System.ComponentModel.DataAnnotations;

namespace Repair.Infrastructure.Models;

    public class RepairCompletedPhoto:BaseEntity
    {
    [Required]
    public List<string> CompletedImages { get; set; } = new();
    [Required]
    public Guid RepairId { get; set; }
    }

