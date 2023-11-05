using PineAPP.Models;

namespace PineAPP.Services.Repositories;

public interface ICardsRepository : IRepository<Card>
{
    int GetTotalCardsInCurrentDeck(Card card);
    int GetCurrentCardIndex(Card card);
}