using PineAPP.Models;

namespace PineAPP.Services.Factories;

public class CardFactory : ICardFactory
{
    public Card CreateCard(string front, string back, string examples = default, int deckId = default)
    {
        return new Card()
        {
            Front = front,
            Back = back,
            Examples = examples,
            DeckId = deckId
        };
    }
}