using Microsoft.EntityFrameworkCore;

namespace ColorAPI.Models;

public static class PrepDb
{
    public static async Task PrepPopulation(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        await SeedData(serviceScope.ServiceProvider.GetService<ColourContext>());
    }

    private static async Task SeedData(ColourContext? colourContext)
    {
        Console.WriteLine("Applying migrations...");
        await colourContext?.Database.MigrateAsync();

        if (!await colourContext.ColourItems.AnyAsync())
        {
            Console.WriteLine("Seeding data...");
            await colourContext.ColourItems.AddRangeAsync(
                new Colour { ColourName = "Red" },
                new Colour { ColourName = "Green" },
                new Colour { ColourName = "Orange" },
                new Colour { ColourName = "Yellow" },
                new Colour { ColourName = "Blue" }
            );
            await colourContext.SaveChangesAsync();
        }
        else
        {
            Console.WriteLine("Data already seeded.");
        }
    }
}