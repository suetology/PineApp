using System.ComponentModel.DataAnnotations;

namespace PineAPP.Models;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required]
    [MaxLength(320)]
    public string Email { get; set; }
    
    [Required]
    [MaxLength(32)]
    public string UserName { get; set; }
    
    [Required]
    [MaxLength(255)]
    public string Password { get; set; }
}