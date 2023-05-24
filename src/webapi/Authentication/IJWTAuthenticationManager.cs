namespace webapi;

public interface IJwtAuthenticationManager
{
    IList<User> Users { get; set; }

    string Authenticate(string email, string password);
}
