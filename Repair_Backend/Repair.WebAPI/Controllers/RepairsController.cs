using Microsoft.AspNetCore.Mvc;
using Repair.BusinessLogic.NotificationBL;
using Repair.BusinessLogic.RepairBL;
using Repair.Infrastructure.DTOs.Repair;
using Repair.Infrastructure.Models;

namespace Repair.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RepairsController : ControllerBase
{
    private readonly IRepairService _repairRepository;
    private readonly INotificationService _notificationService;

    public RepairsController(
        IRepairService repairRepository,
        INotificationService notificationService)
    {
        _repairRepository = repairRepository;
        _notificationService = notificationService;
    }

    [HttpGet]
    public async Task<ActionResult<List<RepairModel>>> GetRepairs()
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
    public async Task<ActionResult<CreateRepairDto>> CreateRepair(CreateRepairDto repair)
    {
        var newRepair = await _repairRepository.AddAsync(repair);
        await _notificationService.NotifyCustomerAsync(newRepair.Id, "received");

        return CreatedAtAction(nameof(GetRepair), new { id = newRepair.Id }, newRepair);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRepair(Guid id, UpdateRepairDto repair)
    {
        if (id != repair.Id) return BadRequest();

        var existingRepair = await _repairRepository.GetByIdAsync(id);
        if (existingRepair == null) return NotFound();

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

    [HttpGet("{id}/received-images")]
    public async Task<IActionResult> GetReceivedImages(Guid id)
    {
        var receivedImages = await _repairRepository.GetReceivedImagesAsync(id);

        if (receivedImages == null)
        {
            return NotFound("Repair not found or no received images.");
        }

        return Ok(receivedImages);
    }

    [HttpGet("{id}/completed-images")]
    public async Task<IActionResult> GetCompletedImages(Guid id)
    {
        var completedImages = await _repairRepository.GetCompletedImagesAsync(id);

        if (completedImages == null)
        {
            return NotFound("Repair not found or no completed images.");
        }

        return Ok(completedImages);
    }

}

public class UpdateStatusRequest
{
    public string Status { get; set; } = string.Empty;
    public string? Notes { get; set; }
}