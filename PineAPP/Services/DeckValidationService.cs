using PineAPP.Exceptions;
using PineAPP.Extensions;
using PineAPP.Models;
using PineAPP.Services.Repositories;

namespace PineAPP.Services;

public class DeckValidationService : IDeckValidationService
{
    private static readonly List<char> ForbiddenCharacters = new() { '_', '@', '&' };
    
    private readonly IDecksRepository _decksRepository;

    public DeckValidationService(IDecksRepository decksRepository)
    {
        _decksRepository = decksRepository;
    }
    
    public void ValidateDeck(Deck deck)
    {
        if (deck.Name.ContainsAnyOfChars(ForbiddenCharacters))
            throw new InvalidDeckDataException("Deck name contains forbidden characters");
            
        if (_decksRepository.DeckExists(deck))
            throw new InvalidDeckDataException("Deck with such name already exists");
    }
}