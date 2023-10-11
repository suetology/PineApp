using System.ComponentModel.DataAnnotations;

namespace PineAPP.Models;

public class Deck : IEquatable<Deck>
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }
    
    [MaxLength(1000)]
    public string Description { get; set; }

    [Required]
    public bool IsPersonal { get; set; }
    
    [Required]
    public int CreatorId { get; set; }

    public List<Card> Cards { get; set; }

    public int Test { get; set; }

    public bool Equals(Deck other)
    {
        if (other == null)
            return false;
        
        return Name == other.Name; 
    }
}