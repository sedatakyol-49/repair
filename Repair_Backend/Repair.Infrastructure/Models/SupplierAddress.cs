using System.ComponentModel.DataAnnotations;

namespace Repair.Infrastructure.Models
{
    public class SupplierAddress:BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Street { get; set; } = string.Empty;

        [MaxLength(50)]
        public string City { get; set; } = string.Empty;

        [MaxLength(50)]
        public string State { get; set; } = string.Empty;

        [MaxLength(20)]
        public string ZipCode { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Country { get; set; } = string.Empty;

        public Supplier Supplier { get; set; }
    }
}
