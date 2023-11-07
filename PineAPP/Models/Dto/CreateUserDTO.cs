using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PineAPP.Models.Dto;

public class CreateUserDTO
{
    [Required]
    [MaxLength(320)]
    public string Email { get; set; }
    
    [Required]
    [MaxLength(32)]
    public string UserName { get; set; }
    
    [Required]
    [MaxLength(255)]
    public string Password { get; set; }

    public static bool IsEmailValid(string email)
    {
        string pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"; 
        return Regex.IsMatch(email, pattern);
    }
}