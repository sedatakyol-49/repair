using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repair.Infrastructure;
using Repair.Infrastructure.DTOs;
using Repair.Infrastructure.Models;


namespace Repair.BusinessLogic.RepairBL;

public class RepairService : IRepairService
{
    private readonly IMapper _mapper;
    private readonly RepairDBContext _context;
    public RepairService(RepairDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<RepairDTO> AddAsync(RepairDTO entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateAsync(RepairDTO entity)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<RepairDTO> GetByIdAsync(Guid id)
    {
        var repairModel= await _context.Repairs.FindAsync(id);
        return _mapper.Map<RepairDTO>(repairModel);
    }

    public async Task<List<RepairDTO>> GetRepairsWithDetailsAsync()
    {
        List<RepairModel>repairModel= await  _context.Repairs
            .Include(r => r.Customer)
            .Include(r => r.Product)
            .Include(r => r.StatusHistory)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();

        return _mapper.Map<List<RepairDTO>>(repairModel);
    }

    public async Task<RepairDTO?> GetRepairWithDetailsAsync(Guid id)
    {
        var repairModel= await _context.Repairs
            .Include(r => r.Customer)
            .Include(r => r.Product)
            .Include(r => r.StatusHistory)
            .FirstOrDefaultAsync(r => r.Id == id);

        return _mapper?.Map<RepairDTO?>(repairModel);
    }

 

    public async Task UpdateRepairStatusAsync(Guid repairId, string status, string? notes)
    {
        var repair = await GetByIdAsync(repairId);
        if (repair == null) return;

        var newStatus = new RepairStatus
        {
            Status = status,
            Timestamp = DateTime.UtcNow,
            Notes = notes,
            RepairId = repairId
        };

        repair.StatusHistory?.Add(newStatus);
        repair.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }
}