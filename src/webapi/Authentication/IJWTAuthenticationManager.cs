namespace webapi;

public interface IJWTAuthenticationManager
{
    IList<User> Users { get; set; }

    string Authenticate(string email, string password);
}
