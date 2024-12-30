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

    public async Task<RepairModel> AddAsync(CreateRepairDto dto)
    {
        var entity = _mapper.Map<RepairModel>(dto);

        if (entity.StatusHistory == null)
        {
            entity.StatusHistory = new List<RepairStatus>();
        }

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

    public async Task UpdateAsync(UpdateRepairDto dto)
    {
        var entity = await _context.Repairs
            .Include(r => r.StatusHistory)
            .FirstOrDefaultAsync(r => r.Id == dto.Id);

        if (entity == null)
        {
            throw new KeyNotFoundException("Repair not found.");
        }

        _mapper.Map(dto, entity);

        _context.Update(entity);
        await _context.SaveChangesAsync();
    }


    public async Task<RepairDto> GetByIdAsync(Guid id)
    {
        var repairModel= await _context.Repairs.FindAsync(id);
        return _mapper.Map<RepairDto>(repairModel);
    }

    public async Task<List<RepairDto>> GetRepairsWithDetailsAsync()
    {
        List<RepairModel>repairModel= await  _context.Repairs
            .Include(r => r.StatusHistory)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();

        return _mapper.Map<List<RepairDto>>(repairModel);
    }

    public async Task<RepairDto?> GetRepairWithDetailsAsync(Guid id)
    {
        var repairModel= await _context.Repairs
            .Include(r => r.StatusHistory)
            .FirstOrDefaultAsync(r => r.Id == id);

        return _mapper?.Map<RepairDto?>(repairModel);
    }



    public async Task UpdateRepairStatusAsync(Guid repairId, string status, string? notes)
    {
        var repair = await _context.Repairs
            .Include(r => r.StatusHistory)
            .FirstOrDefaultAsync(r => r.Id == repairId);

        if (repair == null)
            return;

        var newStatus = new RepairStatus
        {
            Status = status,
            Timestamp = DateTime.UtcNow,
            Notes = notes,
            RepairId = repairId
        };

        repair.StatusHistory ??= new List<RepairStatus>();
        repair.StatusHistory.Add(newStatus);
        repair.UpdatedAt = DateTime.UtcNow;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while saving changes: {ex.Message}");
            throw;
        }
    }

}