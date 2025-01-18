using ColorAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var server = builder.Configuration["DbServer"] ?? "1";
var port = builder.Configuration["DbPort"] ?? "1433";
var user = builder.Configuration["DbUser"] ?? "SA";
var password = builder.Configuration["DbPassword"] ?? "Pa55w0rd2024";
var database = builder.Configuration["Database"] ?? "Colours";
Console.WriteLine(server);
builder.Services.AddDbContext<ColourContext>(options =>
    options.UseSqlServer(
        $"Server={server},{port};Initial Catalog={database};User ID={user};Password={password};TrustServerCertificate=True"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/api/values", () => new[] { "value1, value2" });

app.MapGet("/api/colours", async (ColourContext db) => await db.ColourItems.ToListAsync());
await PrepDb.PrepPopulation(app);

app.Run();

// docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Pa$$w0rd2024' -e 'MSSQL_PID=Express' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest