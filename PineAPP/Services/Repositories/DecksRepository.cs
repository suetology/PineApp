using Microsoft.EntityFrameworkCore;
using PineAPP.Data;
using PineAPP.Models;

namespace PineAPP.Services.Repositories;

public class DecksRepository : Repository<Deck>, IDecksRepository
{
    public DecksRepository(ApplicationDbContext db) 
        : base(db)
    {
    }

    public bool DeckExists(Deck deck)
    {
        return _db.Decks.Any(d => d.Equals(deck));
    }

    public Deck GetDeckByIdWithCards(int id)
    {
        var deck = _db.Decks
            .Where(deck => deck.Id == id)
            .Include(deck => deck.Cards)
            .FirstOrDefault();

        return deck;
    }

    public IEnumerable<Deck> GetAllWithCards()
    {
        var decks = _db.Decks
            .Include(deck => deck.Cards)
            .ToList();

        return decks;
    }

    public IEnumerable<Deck> GetDecksByCreatorId(int creatorId)
    {
        var decks = _db.Decks
            .Where(deck => (deck.IsPersonal && deck.CreatorId == creatorId) || !deck.IsPersonal)
            .Include(deck => deck.Cards).ToList();

        return decks;
    }

    public IEnumerable<Deck> GetPersonalDecks(int creatorId)
    {
        var decks = _db.Decks
            .Where(deck => deck.IsPersonal && deck.CreatorId == creatorId)
            .Include(deck => deck.Cards)
            .ToList();

        return decks;
    }
    
    public IEnumerable<Deck> GetCommunityDecks()
    {
        var decks = _db.Decks
            .Where(deck => !deck.IsPersonal)
            .Include(deck => deck.Cards)
            .ToList();

        return decks;
    }

    public IEnumerable<Deck> Search(string keyword)
    {
        var decks = _db.Decks
            .Where(deck => deck.Name.Contains(keyword))
            .Include(deck => deck.Cards)
            .ToList();

        return decks;
    }
}