
using System.ComponentModel.DataAnnotations;

namespace Repair.Infrastructure.Models;

  public class RepairReceivedPhoto : BaseEntity
   {
    [Required]
    public List<string> ReceivedImages { get; set; } = new();
    [Required]
    public Guid RepairId { get; set; }
   }

