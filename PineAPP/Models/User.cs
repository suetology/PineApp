using System.ComponentModel.DataAnnotations;

namespace PineAPP.Models;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required]
    [MaxLength(320)]
    [MinLength(3)]
    public string Email { get; set; }
    
    [Required]
    [MaxLength(32)]
    [MinLength(3)]
    public string UserName { get; set; }
    
    [Required]
    [MaxLength(255)]
    [MinLength(8)]
    public string Password { get; set; }
}