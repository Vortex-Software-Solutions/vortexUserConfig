
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace vortexUserConfig.Classes;

public class User
{
    private string _name;
    private string _lastName = null;
    private string _userName; 
    private string _email;
    private string _hashedPassword;
    
    public User( string name, string lastName, string userName, string email, string password)
    {
        _name = name;
        _lastName = lastName;
        _userName = userName;
        _email = email;
        _hashedPassword = Hash(password);
    }

    private string Hash(string password)
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

    public bool ComparePassword(string password)
    {
        string hasdedPassword = Hash(password); 
        
        if( _hashedPassword == hasdedPassword)
        {
            return true;
        }

        return false;
    }
    
    
    
}