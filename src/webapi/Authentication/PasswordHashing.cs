using System.Security.Cryptography;
using System.Text;

namespace webapi.Authentication;

public class PasswordHashing
{
    public static string PasswordSHA512(string password)
    {
        StringBuilder stringBuilder = new StringBuilder();
        using (SHA512 sha512 = SHA512.Create())
        {
            byte[] hashValue = sha512.ComputeHash(Encoding.UTF8.GetBytes(password));
            foreach (byte b in hashValue)
            {
                stringBuilder.Append($"{b:X2}");
            }
        }
        return stringBuilder.ToString();
    }
}
