using System.ComponentModel.DataAnnotations;

namespace Repair.Infrastructure
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdateDate { get; set; }
    }
}
