using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repair.Infrastructure.Models;

public class User : BaseEntity
{

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Surname { get; set; } = string.Empty;

    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string FirmName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    [Required]
    public Guid RoleId { get; set; }

    [ForeignKey("RoleId")]
    public Role Role { get; set; }

    public Guid AddressId { get; set; }

    [ForeignKey("AddressId")]
    public UserAddress Address { get; set; }


}

