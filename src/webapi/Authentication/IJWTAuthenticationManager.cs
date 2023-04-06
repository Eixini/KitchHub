namespace webapi;

public interface IJWTAuthenticationManager
{
    string Authenticate(string email, string password);
}
