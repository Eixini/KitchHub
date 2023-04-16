using System.Reflection.Metadata;

namespace webapi;

public class User
{
    public long UserId { get; set; }
    public string NickName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public DateTime RegistrationDate { get; set; }

    public byte[]? Avatar { get; set; }
    public Role Role { get; set; } = null!;
    public DateTime? Birthday { get; set; }
    public UserBan? UserBan { get; set; }
}
