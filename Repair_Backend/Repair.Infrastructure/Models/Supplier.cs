using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repair.Infrastructure.Models
{
    public class Supplier:BaseEntity
    {

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }=string.Empty;

        [MaxLength(100)]
        public string Contact { get; set; }=string.Empty;

        [Required]
        public Guid AddressId { get; set; } 

        [ForeignKey("AddressId")]
        public UserAddress Address { get; set; }
    }
}
