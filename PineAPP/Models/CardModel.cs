namespace PineAPP.Models;

public class CardModel
{
    public CardModel(string front, string back, string examples)
    {
        Front = front;
        Back = back;
        Examples = examples;
    }

    public string Front { get; set; }
    public string Back { get; set; }
    public string Examples { get; set; }
}