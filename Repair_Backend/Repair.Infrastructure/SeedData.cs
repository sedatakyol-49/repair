

using Repair.Infrastructure.Models;

namespace Repair.Infrastructure;

public static class SeedData
{
    public static readonly List<Role> Roles = new()
     {
      new Role
        {
          Id = Guid.Parse("b86d74ba-481a-4e2e-ada6-61aa9e8896e3"),
          Name = "Admin",
          Description = "Administrator role with full permissions",
          CreateDate = DateTime.UtcNow,
          UpdateDate = null
         },
       new Role
        {
          Id = Guid.Parse("c76f378f-1b38-449a-8310-96fafddd5553"),
          Name = "User",
          Description = "Regular user role with limited permissions",
          CreateDate = DateTime.UtcNow,
          UpdateDate = null
        }
    };

    public static readonly List<User> Users = new()
    {
        new User
        {
            Id = Guid.NewGuid(),
            Name = "John",
            Surname = "Doe",
            Username = "johndoe",
            FirmName = "TechCorp",
            Email = "john.doe@gmail.com",
            Password = "hashedpassword123",
            Phone = "123-456-7890",
            RoleId = Guid.Parse("b86d74ba-481a-4e2e-ada6-61aa9e8896e3"),
            CreateDate = DateTime.UtcNow,
            UpdateDate = null
        },
        new User
        {
            Id = Guid.NewGuid(),
            Name = "Jane",
            Surname = "Smith",
            Username = "janesmith",
            FirmName = "InnovateInc",
            Email = "jane.smith@gmail.com",
            Password = "securepassword456",
            Phone = "987-654-3210",
            RoleId = Guid.Parse("c76f378f-1b38-449a-8310-96fafddd5553"),
            CreateDate = DateTime.UtcNow,
            UpdateDate = null
        }
    };

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
            },
            CreateDate = DateTime.UtcNow.AddDays(-3),
            UpdateDate = DateTime.UtcNow.AddDays(-3)
        },
        new RepairModel
        {
            Id = Guid.Parse("d8663e5e-7494-4f81-8739-6e0de1bea7ee"),
            Name = "Sarah",
            Surname="Johnson",
            Email = "sarah@example.com",
            Phone = "555-0456",
            Description = "Won't turn on, possible motherboard issue",
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
            },
            CreateDate = DateTime.UtcNow.AddDays(-4),
            UpdateDate = DateTime.UtcNow.AddDays(-4)
        }
    };

    public static readonly List<RepairReceivedPhoto> repairReceivedPhotos = new(){
          new RepairReceivedPhoto
            {

             Id = Guid.NewGuid(),
             ReceivedImages = new List<string> { "https://example.com/image2.jpg" },
             RepairId= Guid.Parse("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"),
             CreateDate = DateTime.UtcNow,
             UpdateDate = null
           },
              new RepairReceivedPhoto
            {

             Id = Guid.NewGuid(),
             ReceivedImages = new List<string> { "https://example.com/image1.jpg" },
             RepairId= Guid.Parse("d8663e5e-7494-4f81-8739-6e0de1bea7ee"),
             CreateDate = DateTime.UtcNow,
             UpdateDate = null
           }
        };

    public static readonly List<UserAddress> Addresses = new()
{
    new UserAddress
    {
        Id = Guid.Parse("d86c3f87-6cae-4371-b7ba-0a92040063b1"),
        Street = "Main Street 123",
        City = "Berlin",
        State = "Berlin",
        ZipCode = "10115",
        Country = "Germany",
        CreateDate = DateTime.UtcNow,
        UpdateDate = null
    }
};

    public static readonly List<Supplier> Suppliers = new()
{
    new Supplier
    {
        Id = Guid.NewGuid(),
        Name = "Tech Supplies Inc.",
        Contact = "info@techsupplies.com",
        AddressId = Guid.Parse("d86c3f87-6cae-4371-b7ba-0a92040063b1"),
        CreateDate = DateTime.UtcNow,
        UpdateDate = null
    }
};
}

