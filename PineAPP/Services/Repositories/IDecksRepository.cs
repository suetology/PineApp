using PineAPP.Models;

namespace PineAPP.Services.Repositories;

public interface IDecksRepository : IRepository<Deck>
{
    bool DeckExists(Deck deck);
    Deck GetDeckByIdWithCards(int id);
    IEnumerable<Deck> GetAllWithCards();
    IEnumerable<Deck> GetDecksByCreatorId(int creatorId);
    IEnumerable<Deck> GetPersonalDecks(int creatorId);
    IEnumerable<Deck> GetCommunityDecks();
    IEnumerable<Deck> Search(string keyword);
}