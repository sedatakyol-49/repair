using FirstCashSolution.CESOP.WebAPI.Core;
using Microsoft.EntityFrameworkCore;
using NLog.Extensions.Logging;
using Repair.API.Core;
using Repair.API.Services;
using Repair.Infrastructure;
using Newtonsoft.Json;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(o =>
{
    o.Filters.Add<GlobalExceptionHandler>();
}).AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; // Döngüsel referanslar? yok say
    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore; // Null de?erleri serile?tirmeden hariç tut
});

builder.Services.AddMemoryCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors();
builder.Services.AddRepairAutoMapper();

// Database configuration
builder.Services.AddDbContext<RepairDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddRepairBusinessLogic();
builder.Services.AddScoped<DatabaseInitializationService>();
builder.Services.AddLogging(configure => configure
    .SetMinimumLevel(LogLevel.Trace)
    .ClearProviders()
    .AddNLog(new NLogProviderOptions { RemoveLoggerFactoryFilter = false })
    .AddSimpleConsole(conf =>
    {
        conf.SingleLine = true;
        conf.IncludeScopes = false;
        conf.ColorBehavior = Microsoft.Extensions.Logging.Console.LoggerColorBehavior.Enabled;
    })
);
builder.Services.AddHealthChecks();
var app = builder.Build();

app.UseCors(options => options
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
);

using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializationService>();
    await dbInitializer.InitializeAsync();
}

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
SwaggerBuilderExtensions.UseSwagger(app);
app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

app.MapControllers();
app.UseHealthChecks("/health");

app.Run();
