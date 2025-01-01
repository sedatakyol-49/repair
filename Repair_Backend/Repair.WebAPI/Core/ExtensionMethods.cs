using Repair.BusinessLogic.AuthBL;
using Repair.BusinessLogic.NotificationBL;
using Repair.BusinessLogic.RepairBL;
using Repair.BusinessLogic.UserBL;
using Repair.Infrastructure.Mapper;

namespace Repair.API.Core;

/// <summary>
/// Extensions for WebApi.
/// </summary>
public static class ExtensionMethods
{
  
    /// <summary>
    /// Adds the Repair business logic classes.
    /// </summary>
    public static IServiceCollection AddRepairBusinessLogic(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IRepairService, RepairService>();

        return services;
    }

    public static IServiceCollection AddRepairAutoMapper(this IServiceCollection services)
    {
        return services.AddAutoMapper(cfg =>
        {
            cfg.CreateMap<DateTimeOffset, DateTime>().ConvertUsing((dto, dt) => dto.LocalDateTime);
            cfg.CreateMap<string, string>()
                .ConvertUsing(value => (string.IsNullOrWhiteSpace(value) ? null : value)!);

        },
          typeof(RepairProfile)
        );
    }
}


