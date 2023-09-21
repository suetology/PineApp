using Microsoft.EntityFrameworkCore;
using PineAPP.Models;

namespace PineAPP.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options) { }
    
    public DbSet<Deck> Decks { get; set; }
    public DbSet<Card> Cards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Deck>().HasData(
            new Deck
            {
                Id = 1, 
                Name = "Simple Math (Community)", 
                Description = "A few cards to test your basic math skills",
                IsPersonal = false,
                CreatorId = 1
            },
            new Deck
            {
                Id = 2, 
                Name = "Simple Math (Personal)", 
                Description = "A few cards to test your basic math skills",
                IsPersonal = true,
                CreatorId = 1
            }
        );
        
        modelBuilder.Entity<Card>().HasData(
            new Card
            {
                Id = 1, 
                Front = "2 + 2 = ?", 
                Back = "4", 
                Examples = "", 
                DeckId = 1
            },
            new Card
            {
                Id = 2, 
                Front = "5 - 2 = ?", 
                Back = "3", 
                Examples = "", 
                DeckId = 1
            },
            new Card
            {
                Id = 3, 
                Front = "4 * 3 = ?", 
                Back = "12", 
                Examples = "", 
                DeckId = 1
            },
            new Card
            {
                Id = 4, 
                Front = "2 + 2 = ?", 
                Back = "4", 
                Examples = "", 
                DeckId = 2
            },
            new Card
            {
                Id = 5, 
                Front = "5 - 2 = ?", 
                Back = "3", 
                Examples = "", 
                DeckId = 2
            },
            new Card
            {
                Id = 6, 
                Front = "4 * 3 = ?", 
                Back = "12", 
                Examples = "", 
                DeckId = 2
            }
        );
    }
}