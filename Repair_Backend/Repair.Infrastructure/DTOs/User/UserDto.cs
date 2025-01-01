using Repair.Infrastructure.Models;

namespace Repair.Infrastructure.DTOs.User;

public class UserDto
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FirmName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public Guid? AddressId { get; set; }
    public UserAddress Address { get; set; }
    public Guid RoleId { get; set; }
    public Role Role { get; set; }
}