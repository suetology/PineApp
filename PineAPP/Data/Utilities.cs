using PineAPP.Data;
using PineAPP.Models;

public static class Utilities
{
    public static void InitializeDbForTests(ApplicationDbContext db)
    {
        var testUser1 = new User { UserId = 1, UserName = "testuser1", Email = "testuser1@test.com" };
        var testUser2 = new User { UserId = 2, UserName = "testuser2", Email = "testuser2@test.com" };

        var testDeck1 = new Deck { Id = 1, Name = "Test Deck 1", Description = "A test deck", CreatorId = testUser1.UserId };
        var testDeck2 = new Deck { Id = 2, Name = "Test Deck 2", Description = "Another test deck", CreatorId = testUser2.UserId };

        var testCard1 = new Card { Id = 1, Front = "Front of card 1", Back = "Back of card 1", DeckId = testDeck1.Id };
        var testCard2 = new Card { Id = 2, Front = "Front of card 2", Back = "Back of card 2", DeckId = testDeck1.Id };

        // Add entities to DbContext
        db.Users.AddRange(testUser1, testUser2);
        db.Decks.AddRange(testDeck1, testDeck2);
        db.Cards.AddRange(testCard1, testCard2);

        // Save changes to DbContext
        db.SaveChanges();
    }
}