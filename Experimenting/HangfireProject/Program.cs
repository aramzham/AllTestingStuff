using Hangfire;
using HangfireProject;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHangfire(x =>
    x.UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        // .UseInMemoryStorage()
        .UseSqlServerStorage("Server=localhost,1433;Database=Hangfire;User Id=SA;Password=Sa#45678;TrustServerCertificate=True") // don't forget to add reference to Microsoft.Data.SqlClient package
    );

builder.Services.AddHangfireServer(x => x.SchedulePollingInterval = TimeSpan.FromSeconds(5)); // do not do such things in production!!

builder.Services.AddTransient<ExampleJob>();

var app = builder.Build();

app.UseHangfireDashboard();

app.MapGet("/job", (IBackgroundJobClientV2 jobClientV2, IRecurringJobManagerV2 recurringJobManagerV2) =>
{
    // jobClientV2.Enqueue(() => Console.WriteLine("Hello from Background!"));

    // jobClientV2.Schedule(() => Console.WriteLine("Hello from scheduled job"), TimeSpan.FromSeconds(10));
    
    // recurringJobManagerV2.AddOrUpdate("test", () => Console.WriteLine("Hello from recurring job"), Cron.MinuteInterval(5));

    jobClientV2.Schedule<ExampleJob>(x => x.Execute(), TimeSpan.FromSeconds(5));
    return Results.Ok("Hello World!");
});

app.Run();
