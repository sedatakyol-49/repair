using Microsoft.AspNetCore.Mvc;
using Repair.API.Models;
using Repair.API.Repositories;
using Repair.API.Services;

namespace Repair.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RepairsController : ControllerBase
{
    private readonly IRepairRepository _repairRepository;
    private readonly INotificationService _notificationService;

    public RepairsController(
        IRepairRepository repairRepository,
        INotificationService notificationService)
    {
        _repairRepository = repairRepository;
        _notificationService = notificationService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RepairModel>>> GetRepairs()
    {
        var repairs = await _repairRepository.GetRepairsWithDetailsAsync();
        return Ok(repairs);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RepairModel>> GetRepair(Guid id)
    {
        var repair = await _repairRepository.GetRepairWithDetailsAsync(id);
        if (repair == null) return NotFound();
        return Ok(repair);
    }

    [HttpPost]
    public async Task<ActionResult<RepairModel>> CreateRepair(RepairModel repair)
    {
        repair.CreatedAt = DateTime.UtcNow;
        repair.UpdatedAt = DateTime.UtcNow;

        var newRepair = await _repairRepository.AddAsync(repair);
        await _notificationService.NotifyCustomerAsync(newRepair.Id, "received");

        return CreatedAtAction(nameof(GetRepair), new { id = newRepair.Id }, newRepair);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRepair(Guid id, RepairModel repair)
    {
        if (id != repair.Id) return BadRequest();

        var existingRepair = await _repairRepository.GetByIdAsync(id);
        if (existingRepair == null) return NotFound();

        repair.UpdatedAt = DateTime.UtcNow;
        await _repairRepository.UpdateAsync(repair);

        return NoContent();
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateStatusRequest request)
    {
        var repair = await _repairRepository.GetByIdAsync(id);
        if (repair == null) return NotFound();

        await _repairRepository.UpdateRepairStatusAsync(id, request.Status, request.Notes);
        await _notificationService.NotifyCustomerAsync(id, request.Status);

        return NoContent();
    }
}

public class UpdateStatusRequest
{
    public string Status { get; set; } = string.Empty;
    public string? Notes { get; set; }
}