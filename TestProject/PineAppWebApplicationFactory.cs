using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using PineAPP.Data;
using PineAPP.Exceptions;
using PineAPP.Models;

namespace PineApp.TestProject;

public class PineAppWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }
            
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
                options.EnableSensitiveDataLogging();
            });
            
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
            });
            
            var serviceProvider = services.BuildServiceProvider();
            
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<ApplicationDbContext>();
                
                db.Database.EnsureCreated();
                
                SeedTestData(db);
            }
        });
    }

    private void SeedTestData(ApplicationDbContext db)
    {
        // Check if the Users already exist
        var existingUser1 = db.Users.Find(1);
        var existingUser2 = db.Users.Find(2);
    
        var testUser1 = new User { UserId = 1, UserName = "testuser1", Email = "testuser1@test.com", Password = "321lolpassword"};
        var testUser2 = new User { UserId = 2, UserName = "testuser2", Email = "testuser2@test.com", Password = "passwordlol123" };

        // Similarly check for Decks
        var existingDeck1 = db.Decks.Find(1);
        var existingDeck2 = db.Decks.Find(2);

        var testDeck1 = new Deck { Id = 1, Name = "Test Deck 1", Description = "A test deck", CreatorId = testUser1.UserId };
        var testDeck2 = new Deck { Id = 2, Name = "Test Deck 2", Description = "Another test deck", CreatorId = testUser2.UserId };

        // Check for Cards
        var existingCard1 = db.Cards.Find(1);
        var existingCard2 = db.Cards.Find(2);

        var testCard1 =  new Card { Id = 1, Front = "Front of card 1", Back = "Back of card 1", DeckId = testDeck1.Id };
        var testCard2 =  new Card { Id = 2, Front = "Front of card 2", Back = "Back of card 2", DeckId = testDeck1.Id };

        // Add or update entities as necessary
        if (existingUser1 == null) db.Users.Add(testUser1);
        if (existingUser2 == null) db.Users.Add(testUser2);

        if (existingDeck1 == null) db.Decks.Add(testDeck1);
        if (existingDeck2 == null) db.Decks.Add(testDeck2);

        if (existingCard1 == null) db.Cards.Add(testCard1);
        if (existingCard2 == null) db.Cards.Add(testCard2);

        db.SaveChanges();
    }
}