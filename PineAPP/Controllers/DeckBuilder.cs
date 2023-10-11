using PineAPP.Exceptions;
using PineAPP.Extensions;
using PineAPP.Models;
using PineAPP.Models.Dto;

namespace PineAPP.Controllers;

public class DeckBuilder
{
    private static readonly List<char> _forbiddenCharacters = new() { '_', '@', '&' };
    
    private readonly int _userId;
    
    public DeckBuilder(int userId)
    {
        _userId = userId;
    }
    
    public Deck CreateDeckFromString(string data)
    {
        var lines = data.Split(Environment.NewLine);

        if (lines.Length < 3)
            throw new InvalidFormatException("Data does not contain enough information about deck");

        var name = lines[0];
        if (ContainsForbiddenCharacters(name))
            throw new InvalidFormatException("Deck name contains invalid symbols");
        
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
        
        var deck = new Deck
        {
            Name = name,
            Description = description,
            IsPersonal = isPersonal,
            CreatorId = _userId,      //temp
            Cards = cards
        };

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

        var card = new Card
        {
            Front = front,
            Back = back,
            Examples = examples
        };

        return card;
    }
    
    public static bool ContainsForbiddenCharacters(string name)
    {
        return name.ContainsAnyOfChars(_forbiddenCharacters);
    }
}