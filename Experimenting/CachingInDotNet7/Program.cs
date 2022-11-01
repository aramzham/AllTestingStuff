using CachingInDotNet7.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CandidateDbContext>(o=>o.UseInMemoryDatabase("Candidate.Database"));
builder.Services.AddOutputCache();

var app = builder.Build();

app.UseOutputCache();

app.MapGet("/candidates", (CandidateDbContext db) => db.Candidates);

app.MapGet("/candidate/{id:int}", async (CandidateDbContext db, int id) =>
{
    var candidate = await db.Candidates.FirstOrDefaultAsync(x => x.Id == id);
    return candidate is null ? Results.NotFound() : Results.Ok(candidate);
});

app.MapPost("/candidate", async (CandidateDbContext db, Candidate candidate) =>
{
    await db.Candidates.AddAsync(candidate);
    await db.SaveChangesAsync();

    return Results.Created($"/candidate/{candidate.Id}", candidate);
});

app.Run();