using Repair.Infrastructure.DTOs.Repair;
using Repair.Infrastructure.Models;

namespace Repair.BusinessLogic.RepairBL;

public interface IRepairService
{

    Task<RepairDto> GetByIdAsync(Guid id);
    Task<RepairModel> AddAsync(CreateRepairDto entity);
    Task UpdateAsync(UpdateRepairDto entity);
    Task DeleteAsync(Guid id);
    Task<List<RepairDto>> GetRepairsWithDetailsAsync();
    Task<RepairDto?> GetRepairWithDetailsAsync(Guid id);
    Task UpdateRepairStatusAsync(Guid repairId, string status, string? notes);

    Task<List<string>?> GetReceivedImagesAsync(Guid repairId);
    Task<List<string>?> GetCompletedImagesAsync(Guid repairId);
}