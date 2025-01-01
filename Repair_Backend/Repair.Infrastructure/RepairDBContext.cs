using Microsoft.EntityFrameworkCore;
using Repair.Infrastructure.Models;


namespace Repair.Infrastructure;

public class RepairDBContext : DbContext
{
    public RepairDBContext(DbContextOptions<RepairDBContext> options) : base(options)
    {
    }

    public DbSet<RepairModel> Repairs { get; set; } = null!;
    public DbSet<RepairStatus> RepairStatuses { get; set; } = null!;
    public DbSet<RepairReceivedPhoto> RepairReceivedPhotos { get; set; } = null!;
    public DbSet<RepairCompletedPhoto> RepairCompletedPhotos { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;

}