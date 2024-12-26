using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repair.API.Models;

namespace Repair.API.Data;

public class RepairContext : DbContext
{
    public RepairContext(DbContextOptions<RepairContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<RepairModel> Repairs { get; set; } = null!;
    public DbSet<RepairStatus> RepairStatuses { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var stringListComparer = new ValueComparer<List<string>>(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.ToList());

        modelBuilder.Entity<RepairModel>()
            .Property(r => r.ReceivedImages)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList())
            .Metadata.SetValueComparer(stringListComparer);

        modelBuilder.Entity<RepairModel>()
            .Property(r => r.CompletedImages)
            .HasConversion(
                v => v != null ? string.Join(',', v) : null,
                v => v != null ? v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList() : null)
            .Metadata.SetValueComparer(stringListComparer);
    }



}