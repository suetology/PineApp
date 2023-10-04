using System.ComponentModel.DataAnnotations;

namespace PineAPP.Models.Dto;

public class CreateCardDTO
{
    [Required]
    [MaxLength(255)]
    public string Front { get; set;}

    [Required]
    public string Back { get; set;}

    public int DeckId {get; set;}
    
}