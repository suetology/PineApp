using System.ComponentModel.DataAnnotations;

namespace PineAPP.Models.Dto;

public class CreateDeckDTO
{
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }
    
    [MaxLength(1000)]
    public string Description { get; set; }

    [Required]
    public bool IsPersonal { get; set; }
    
    [Required]
    public int CreatorId { get; set; }
    
    public int Correct { get; set; }

    public int Wrong { get; set; }
    
    //public List<Card> Cards { get; set; }
}