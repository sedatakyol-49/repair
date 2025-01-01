using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Repair.Infrastructure.Models;
    public class Role : BaseEntity
    {
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }=string.Empty;

    [MaxLength(255)]
    public string Description { get; set; } = string.Empty;

    public ICollection<User> Users { get; set; }
     }

