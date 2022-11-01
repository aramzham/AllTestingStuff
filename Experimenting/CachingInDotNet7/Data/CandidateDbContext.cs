using Microsoft.EntityFrameworkCore;

namespace CachingInDotNet7.Data;

public class CandidateDbContext : DbContext
{
    public CandidateDbContext(DbContextOptions<CandidateDbContext> options) : base(options)
    {

    }

    public DbSet<Candidate> Candidates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidate>().HasData(new Candidate()
        {
            Address = "Yerevan, Charbax",
            Age = 33,
            Country = "France",
            EmailAddress = "biography@labelsmart.ru",
            FamilyName = "Anthony",
            Hired = true,
            Id = 1,
            Name = "Carmelo"
        });

        modelBuilder.Entity<Candidate>().HasData(new Candidate()
        {
            Address = "Kiev",
            Age = 30,
            Country = "Ukraine",
            EmailAddress = "mortal.combat@gmail.com",
            FamilyName = "Pozov",
            Hired = false,
            Id = 2,
            Name = "Roman"
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("Candidate.Database");
    }
}