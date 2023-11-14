using PineAPP.Models;

namespace PineAPP.Services;

public interface IDeckValidationService
{
    void ValidateDeck(Deck deck);
    void CheckForForbiddenCharacters(Deck deck);
    void CheckForNameAvailability(Deck deck);


}