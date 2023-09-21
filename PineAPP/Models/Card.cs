using System.ComponentModel.DataAnnotations;

namespace PineAPP.Models;

public class Card
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Front { get; set; }
    [Required]
    [MaxLength(255)]
    public string Back { get; set; }
    [MaxLength(1000)]
    public string Examples { get; set; }

    public int DeckId { get; set; }
}