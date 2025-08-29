
using Serilog;
using WorkerService1;

var builder = Host.CreateApplicationBuilder(args);

// ✅ Configure Serilog before building the host
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug().WriteTo.Console()
    .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// ✅ Add Serilog to logging pipeline
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();
// ✅ Configure Worker Service to run as Windows Service
builder.Services.AddWindowsService(options =>
{
    options.ServiceName = ".NET Test Service"; // 👈 set Windows Service name here
});

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
