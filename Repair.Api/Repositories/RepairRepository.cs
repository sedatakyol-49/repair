using Microsoft.EntityFrameworkCore;
using Repair.API.Data;
using Repair.API.Models;

namespace Repair.API.Repositories;

public class RepairRepository : Repository<RepairModel>, IRepairRepository
{
    public RepairRepository(RepairContext context) : base(context)
    {
    }

    public async Task<IEnumerable<RepairModel>> GetRepairsWithDetailsAsync()
    {
        return await _context.Repairs
            .Include(r => r.Customer)
            .Include(r => r.Product)
            .Include(r => r.StatusHistory)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }

    public async Task<RepairModel?> GetRepairWithDetailsAsync(Guid id)
    {
        return await _context.Repairs
            .Include(r => r.Customer)
            .Include(r => r.Product)
            .Include(r => r.StatusHistory)
            .FirstOrDefaultAsync(r => r.Id == id);
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

        repair.StatusHistory.Add(newStatus);
        repair.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }
}