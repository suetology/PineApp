using System.ComponentModel.DataAnnotations;

namespace NotificationsService.Models;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required]
    [MaxLength(320)]
    [MinLength(3)]
    public string Email { get; set; }
}