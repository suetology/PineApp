using PineAPP.Models;

namespace PineAPP.Services;

public interface IDeckBuilderService
{
    Deck CreateDeckFromString(string data);
    Card CreateCardFromString(string data);
}