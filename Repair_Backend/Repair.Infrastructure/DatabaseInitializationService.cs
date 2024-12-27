using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repair.Infrastructure;


namespace Repair.API.Services;

public class DatabaseInitializationService
{
    private readonly RepairDBContext _context;
    private readonly ILogger<DatabaseInitializationService> _logger;

    public DatabaseInitializationService(
        RepairDBContext context,
        ILogger<DatabaseInitializationService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task InitializeAsync()
    {
        try
        {

            await _context.Database.EnsureCreatedAsync();

            // Check if database is empty
            if (!await _context.Customers.AnyAsync())
            {
                _logger.LogInformation("Database is empty. Seeding initial data...");

                // Add customers
                await _context.Customers.AddRangeAsync(SeedData.Customers);
                await _context.SaveChangesAsync();

                // Add repairs with status history
                await _context.Repairs.AddRangeAsync(SeedData.Repairs);
                await _context.SaveChangesAsync();

                // Add products
                await _context.Products.AddRangeAsync(SeedData.Products);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Database seeding completed successfully.");
            }
            else
            {
                _logger.LogInformation("Database already contains data. Skipping seed.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }
}