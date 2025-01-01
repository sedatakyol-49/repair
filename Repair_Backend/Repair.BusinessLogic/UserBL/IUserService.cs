using Repair.Infrastructure.DTOs;
using Repair.Infrastructure.DTOs.User;
using Repair.Infrastructure.Models;

namespace Repair.BusinessLogic.UserBL
{
    public interface IUserService
    {
        Task<UserDto> GetByIdAsync(Guid id);
        Task<UserDto?> GetUserWithDetailsAsync(Guid id);
        Task<List<UserDto>> GetUsersWithDetailsAsync();
        Task<User>AddUserAsync(UserDto dto);
        Task UpdateUserAsync(UpdateUserDto entity);
        Task DeleteUserAsync(Guid id);
    }
}
