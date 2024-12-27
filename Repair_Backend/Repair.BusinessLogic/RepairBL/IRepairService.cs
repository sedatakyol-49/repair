using Repair.Infrastructure.DTOs;

namespace Repair.BusinessLogic.RepairBL;

public interface IRepairService
{

    Task<RepairDTO> GetByIdAsync(Guid id);
    Task<RepairDTO> AddAsync(RepairDTO entity);
    Task UpdateAsync(RepairDTO entity);
    Task DeleteAsync(Guid id);
    Task<List<RepairDTO>> GetRepairsWithDetailsAsync();
    Task<RepairDTO?> GetRepairWithDetailsAsync(Guid id);
    Task UpdateRepairStatusAsync(Guid repairId, string status, string? notes);
}