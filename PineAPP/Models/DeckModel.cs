namespace PineAPP.Models;

public class DeckModel
{
    public DeckModel(int id, string name, string description, List<CardModel> cards)
    {
        Id = id;
        Name = name;
        Description = description;
        Cards = cards;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<CardModel> Cards { get; set; }
    
}