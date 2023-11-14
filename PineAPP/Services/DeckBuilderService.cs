using PineAPP.Exceptions;
using PineAPP.Extensions;
using PineAPP.Models;
using PineAPP.Models.Dto;
using PineAPP.Services.Factories;

namespace PineAPP.Services;

public class DeckBuilderService : IDeckBuilderService
{
    private readonly IDeckFactory _deckFactory;
    private readonly ICardFactory _cardFactory;

    public DeckBuilderService(
        IDeckFactory deckFactory, 
        ICardFactory cardFactory)
    {
        _deckFactory = deckFactory;
        _cardFactory = cardFactory;
    }
    
    public Deck CreateDeckFromString(string data)
    {
        var lines = data.Split(Environment.NewLine);
        
        if (data.Length < 3)
            throw new InvalidFormatException("Data does not contain enough information about deck");
        
        var name = lines[0];
        
        var description = lines[1];
        var isPersonalStr = lines[2];

        var isPersonal = isPersonalStr switch
        {
            "Personal" => true,
            "Community" => false,
            _ => throw new InvalidFormatException("Unable to specify if deck is either personal or community")
        };

        var cards = new List<Card>();
        for (var i = 3; i < lines.Length; i++)
        {
            var card = CreateCardFromString(lines[i]);
            cards.Add(card);
        }

        var deck = _deckFactory.CreateDeck(
            name: name, 
            description: description, 
            isPersonal: isPersonal, 
            creatorId: 1, 
            cards: cards); // temp hardcoded creatorId

        return deck;
    }

    public Card CreateCardFromString(string data)
    {
        var parts = data.Split('\\');

        if (parts.Length is < 2 or > 3)
            throw new InvalidFormatException("Data does not contain enough information about card");

        var front = parts[0];
        var back = parts[1];
        var examples = parts.Length == 3 ? parts[2] : "";

        var card = _cardFactory.CreateCard(front, back, examples);

        return card;
    }
}