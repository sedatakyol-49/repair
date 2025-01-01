using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repair.Infrastructure;
using Repair.Infrastructure.DTOs.Repair;
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

        var receivedEntity = new RepairReceivedPhoto()
        {
            Id = Guid.NewGuid(),
            ReceivedImages = dto.ReceivedImages ?? [],
            RepairId = entity.Id,
        };

        await _context.AddAsync(receivedEntity);
        await _context.SaveChangesAsync();

        var completedEntity = new RepairCompletedPhoto()
        {
            Id = Guid.NewGuid(),
            CompletedImages = dto.CompletedImages ?? [],
            RepairId = entity.Id,
        };

        await _context.AddAsync(completedEntity);
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


        var existingReceivedImages = _context.RepairReceivedPhotos
            .Where(img => img.RepairId == dto.Id);
        _context.RepairReceivedPhotos.RemoveRange(existingReceivedImages);

        var newReceivedImages = dto.ReceivedImages?.Select(image => new RepairReceivedPhoto
        {
            Id = Guid.NewGuid(),
            ReceivedImages = dto.ReceivedImages,
            RepairId = dto.Id,
        });
        await _context.RepairReceivedPhotos.AddRangeAsync(newReceivedImages);

        var existingCompletedImages = _context.RepairCompletedPhotos
    .Where(img => img.RepairId == dto.Id);
        _context.RepairCompletedPhotos.RemoveRange(existingCompletedImages);


        var newCompletedImages = dto.CompletedImages?.Select(image => new RepairCompletedPhoto
        {
            Id = Guid.NewGuid(),
            CompletedImages = dto.CompletedImages,
            RepairId = dto.Id,
        });

        await _context.RepairCompletedPhotos.AddRangeAsync(newCompletedImages);

        _context.Update(entity);
        await _context.SaveChangesAsync();
    }


    public async Task<RepairDto> GetByIdAsync(Guid id)
    {
        var repairModel = await _context.Repairs.FindAsync(id);
        return _mapper.Map<RepairDto>(repairModel);
    }

    public async Task<List<RepairDto>> GetRepairsWithDetailsAsync()
    {
        List<RepairModel> repairModel = await _context.Repairs
                     .AsNoTracking()
                     .Include(r => r.StatusHistory)
                     .OrderByDescending(r => r.CreateDate)
                     .ToListAsync();

        return _mapper.Map<List<RepairDto>>(repairModel);
    }

    public async Task<RepairDto?> GetRepairWithDetailsAsync(Guid id)
    {
        var repairModel = await _context.Repairs
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
        repair.UpdateDate = DateTime.UtcNow;

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

    public async Task<List<string>?> GetReceivedImagesAsync(Guid repairId)
    {
        return await _context.RepairReceivedPhotos
            .Where(r => r.RepairId == repairId)
            .Select(r => r.ReceivedImages)
            .FirstOrDefaultAsync();
    }

    public async Task<List<string>?> GetCompletedImagesAsync(Guid repairId)
    {
        return await _context.RepairCompletedPhotos
            .Where(r => r.RepairId == repairId)
            .Select(r => r.CompletedImages)
            .FirstOrDefaultAsync();
    }
}