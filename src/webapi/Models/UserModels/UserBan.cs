using System.ComponentModel.DataAnnotations.Schema;

namespace webapi;

public class UserBan
{
    public long Id { get; set; }
    public DateTime BanTime { get; set; }
    public DateTime BanDuration { get; set; }
    public string BanCause { get; set; } = null!;

    [ForeignKey("IssuerId")]
    public User WhoBanned { get; set; }
    public long? IssuerId { get; set; } = null!;
}