using PineAPP.Data;
using PineAPP.Models;

namespace PineAPP.Services.Repositories;

public class CardsRepository : Repository<Card>, ICardsRepository
{
    public CardsRepository(ApplicationDbContext db) 
        : base(db)
    {
    }

    public int GetTotalCardsInCurrentDeck(Card card)
    {
        return _db.Cards.Count(c => c.DeckId == card.DeckId);
    }

    public int GetCurrentCardIndex(Card card)
    {
        return _db.Cards.Count(c => c.DeckId == card.DeckId && c.Id <= card.Id);
    }
}