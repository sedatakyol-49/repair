using Repair.API.Models;

namespace Repair.API.Repositories;

public interface IRepairRepository : IRepository<RepairModel>
{
    Task<IEnumerable<RepairModel>> GetRepairsWithDetailsAsync();
    Task<RepairModel?> GetRepairWithDetailsAsync(Guid id);
    Task UpdateRepairStatusAsync(Guid repairId, string status, string? notes);
}