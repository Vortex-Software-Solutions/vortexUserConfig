using System.Security.Cryptography;
using System.Text;

namespace vortexUserConfig.UsersConfig.Presentation.Services.Authentication;


public class PasswordService
{
    public string Hash(string password)
    {
        byte[] inputBytes = Encoding.UTF8.GetBytes(password);

        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}