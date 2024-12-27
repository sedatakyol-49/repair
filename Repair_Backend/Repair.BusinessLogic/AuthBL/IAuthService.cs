namespace Repair.BusinessLogic.AuthBL;

public interface IAuthService
{
    Task<bool> BusinessExistsAsync();
    Task RegisterAsync(string email, string password, string businessName);
    Task<string> LoginAsync(string email, string password);
    Task<bool> ResetPasswordAsync(string email);
    Task<bool> UpdatePasswordAsync(string email, string tempPassword, string newPassword);
}