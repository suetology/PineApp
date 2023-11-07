using PineAPP.Models;

namespace PineAPP.Services.Factories;

public interface IDeckFactory
{
    Deck CreateDeck(string name, string description, bool isPersonal, int creatorId, IEnumerable<Card> cards = default);
}