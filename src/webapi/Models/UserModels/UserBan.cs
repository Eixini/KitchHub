namespace webapi;

public class UserBan
{
    public DateTime BanTime { get; set; }
    public DateTime BanDuration { get; set; }
    public string BanCause { get; set; } = null!;
    public User WhoBanned { get; set; } = null!;

}