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

    public async Task<RepairDTO> AddAsync(RepairDTO dto)
    {
        var entity = _mapper.Map<RepairModel>(dto);

        if (entity.StatusHistory == null)
        {
            entity.StatusHistory = new List<RepairStatus>();
        }

        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();

        dto.Id = entity.Id;
        return dto;
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

    public async Task UpdateAsync(RepairDTO dto)
    {
        var entity = await _context.Repairs
             .Include(r => r.StatusHistory)
             .FirstOrDefaultAsync(r => r.Id == dto.Id);

        if (entity == null)
        {
            throw new KeyNotFoundException("Repair not found.");
        }

        _mapper.Map(dto, entity);

        if (dto.StatusHistory != null)
        {
            entity.StatusHistory.Clear();

            foreach (var statusDto in dto.StatusHistory)
            {
                entity.StatusHistory.Add(new RepairStatus
                {
                    Id = statusDto.Id != Guid.Empty ? statusDto.Id : Guid.NewGuid(), 
                    Status = statusDto.Status,
                    Timestamp = statusDto.Timestamp,
                    Notes = statusDto.Notes,
                    RepairId = entity.Id 
                });
            }
        }

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
            .Include(r => r.StatusHistory)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();

        return _mapper.Map<List<RepairDTO>>(repairModel);
    }

    public async Task<RepairDTO?> GetRepairWithDetailsAsync(Guid id)
    {
        var repairModel= await _context.Repairs
            .Include(r => r.StatusHistory)
            .FirstOrDefaultAsync(r => r.Id == id);

        return _mapper?.Map<RepairDTO?>(repairModel);
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