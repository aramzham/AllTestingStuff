using BenchmarkDotNet.Running;
using CachingInDotNet7;
using CachingInDotNet7.Data;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CandidateDbContext>(o => o.UseInMemoryDatabase("Candidate.Database"));
builder.Services.AddOutputCache(o =>
{
    // o.AddBasePolicy(x => x.NoCache()); // it's default behaviour
    o.AddBasePolicy(x =>
        x.With(xx => xx.HttpContext.Request.Headers["nocache"] == "true")
            .NoCache()); // no caching for this kind of requests
    o.AddPolicy("nocache", x => x.NoCache());
});

var app = builder.Build();

app.UseOutputCache();

app.MapGet("/candidates", [OutputCache(PolicyName = "nocache")](CandidateDbContext db) => db.Candidates);

app.MapGet("/candidate/{id:int}", async (CandidateDbContext db, int id) =>
{
    var candidate = await db.Candidates.FirstOrDefaultAsync(x => x.Id == id);
    return candidate is null ? Results.NotFound() : Results.Ok(candidate);
}).CacheOutput();

app.MapGet("/candidate", async (CandidateDbContext db, string name) =>
{
    var candidate = await db.Candidates.FirstOrDefaultAsync(x => x.Name == name);
    return candidate is null ? Results.NotFound() : Results.Ok(candidate);
}).CacheOutput(x => x.SetVaryByQuery("name").Expire(TimeSpan.FromSeconds(10)));

app.MapPost("/candidate", async (CandidateDbContext db, Candidate candidate) =>
{
    await db.Candidates.AddAsync(candidate);
    await db.SaveChangesAsync();

    return Results.Created($"/candidate/{candidate.Id}", candidate);
});

var count = 0;
app.MapGet("/lock", async (context) =>
{
    await 1;
    // await TimeSpan.FromSeconds(1);
    await context.Response.WriteAsync(count++.ToString());
}).CacheOutput(x => x.SetLocking(false).Expire(TimeSpan.FromMicroseconds(1)));
// when you set this parameter value to false, you don't lock this method in case of multiple simultaneous requests

app.MapGet("/range", (HttpContext context, int endNumber) =>
{
    foreach (var i in 1..endNumber)
    {
        context.Response.WriteAsync($"{i}{Environment.NewLine}").GetAwaiter().GetResult();
    }
});

app.MapGet("/moreElegantRange", (HttpContext context, int number) =>
{
    foreach (var i in number)
    {
        context.Response.WriteAsync($"{i}{Environment.NewLine}").GetAwaiter().GetResult();
    }
});

// uncomment this line to see the comparison of different loops
// BenchmarkRunner.Run<Benchmarks>();

app.Run();