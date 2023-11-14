using PineAPP.Models;

namespace PineAPP.Services.Factories;

public class DeckFactory : IDeckFactory
{
    private readonly IDeckValidationService _deckValidationService;

    public DeckFactory(IDeckValidationService deckValidationService)
    {
        _deckValidationService = deckValidationService;
    }

    public Deck CreateDeck(string name, string description, bool isPersonal, int creatorId, IEnumerable<Card> cards = default)
    {   
        var deck = new Deck()
        {
            Name = name,
            Description = description,
            IsPersonal = isPersonal,
            CreatorId = creatorId,
            Cards = cards?.ToList()
        };

        _deckValidationService.ValidateDeck(deck);

        return deck;
    }
}