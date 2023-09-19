
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;
public class RegisterDto
{
    [Required]
    public string UserName { get; set; }
    public string Password { get; set; }
}


