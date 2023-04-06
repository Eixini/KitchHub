using System.ComponentModel.DataAnnotations;

namespace webapi;

public class Reg
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string NickName { get; set; }
    [Required]
    public string Password { get; set; }
}
