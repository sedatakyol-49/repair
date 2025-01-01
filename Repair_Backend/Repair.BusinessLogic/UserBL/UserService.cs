using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repair.BusinessLogic.AuthBL;
using Repair.Infrastructure;
using Repair.Infrastructure.DTOs;
using Repair.Infrastructure.DTOs.User;
using Repair.Infrastructure.Models;

namespace Repair.BusinessLogic.UserBL
{
    public class UserService : IUserService
    {

        private readonly RepairDBContext _context;
        private readonly ILogger<AuthService> _logger;
        private readonly IMapper _mapper;

        public UserService(RepairDBContext context, ILogger<AuthService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<User> AddUserAsync(UserDto dto)
        {
            var entity = _mapper.Map<User>(dto);

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<List<UserDto>> GetUsersWithDetailsAsync()
        {
            List<User> users = await _context.Users
              .AsNoTracking()
              .Include(r => r.Role)
              .Include(r => r.Address)
              .OrderByDescending(r => r.CreateDate)
              .ToListAsync();

            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto?> GetUserWithDetailsAsync(Guid id)
        {
            var userModel = await _context.Users
      .Include(r => r.Address)
      .Include(r => r.Role)
      .FirstOrDefaultAsync(r => r.Id == id);

            return _mapper?.Map<UserDto?>(userModel);
        }

        public async Task UpdateUserAsync(UpdateUserDto dto)
        {
            var entity = await _context.Users
        .FirstOrDefaultAsync(r => r.Id == dto.RoleId);

            if (entity != null)
            {
                _context.Update(entity);
                await _context.SaveChangesAsync();
            }

        }
    }
}
