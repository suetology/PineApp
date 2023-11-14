using PineAPP.Models;

namespace PineAPP.Services.Factories;

public interface ICardFactory
{
    Card CreateCard(string front, string back, string examples = default, int deckId = default);
}