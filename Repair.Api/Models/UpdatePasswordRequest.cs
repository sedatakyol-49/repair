namespace Repair.API.Models;

public class UpdatePasswordRequest
{
    public string Email { get; set; } = string.Empty;
    public string TempPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}