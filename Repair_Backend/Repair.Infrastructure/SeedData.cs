

using Repair.Infrastructure.Models;

namespace Repair.Infrastructure;

public static class SeedData
{

    public static readonly List<RepairModel> Repairs = new()
    {
        new RepairModel
        {
            Id = Guid.Parse("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"),
            Name = "John",
            Surname = "Smith",
            Email = "john@example.com",
            Phone = "555-0123",
            Description = "Screen is cracked and battery drains quickly",
            ReceivedImages = new List<string> { "https://example.com/image1.jpg" },
            CreatedAt = DateTime.UtcNow.AddDays(-3),
            UpdatedAt = DateTime.UtcNow.AddDays(-3),
            EstimatedCompletionDate = DateTime.UtcNow.AddDays(2),
            AppointmentDate = DateTime.UtcNow.AddDays(-3),
            AppointmentTime = "10:00",
            Type = "phone",
            Brand = "iPhone",
            Model = "13 Pro",
            SerialNumber = "ABC123",
            StatusHistory = new List<RepairStatus>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Status = "received",
                    Timestamp = DateTime.UtcNow.AddDays(-3),
                    Notes = "Device received with visible screen damage",
                    RepairId = Guid.Parse("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b")
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Status = "diagnosing",
                    Timestamp = DateTime.UtcNow.AddDays(-2),
                    Notes = "Screen replacement needed, battery tests in progress",
                    RepairId = Guid.Parse("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b")
                }
            }
        },
        new RepairModel
        {
            Id = Guid.Parse("d8663e5e-7494-4f81-8739-6e0de1bea7ee"),
            Name = "Sarah",
            Surname="Johnson",
            Email = "sarah@example.com",
            Phone = "555-0456",
            Description = "Won't turn on, possible motherboard issue",
            ReceivedImages = new List<string> { "https://example.com/image2.jpg" },
            CreatedAt = DateTime.UtcNow.AddDays(-4),
            UpdatedAt = DateTime.UtcNow.AddDays(-4),
            EstimatedCompletionDate = DateTime.UtcNow.AddDays(3),
            AppointmentDate = DateTime.UtcNow.AddDays(-4),
            AppointmentTime = "14:30",
                 Type = "laptop",
            Brand = "Dell",
            Model = "XPS 15",
            SerialNumber = "XYZ789",
            StatusHistory = new List<RepairStatus>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Status = "received",
                    Timestamp = DateTime.UtcNow.AddDays(-4),
                    Notes = "Device won't power on",
                    RepairId = Guid.Parse("d8663e5e-7494-4f81-8739-6e0de1bea7ee")
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Status = "diagnosing",
                    Timestamp = DateTime.UtcNow.AddDays(-3),
                    Notes = "Initial diagnosis shows potential power supply issue",
                    RepairId = Guid.Parse("d8663e5e-7494-4f81-8739-6e0de1bea7ee")
                }
            }
        }
    };
}