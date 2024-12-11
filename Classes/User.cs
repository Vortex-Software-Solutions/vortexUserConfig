
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace vortexUserConfig.Classes;

public class User
{
    private string _name;
    private string _lastName;
    private string _userName; 
    private string _email;
    private string _hashedPassword;
    
    //Made to create base user that contains the basic information
    public User(string name, string lastName, string userName, string email, string password)
    {
        _name = name;
        _lastName = lastName;
        _userName = userName;
        _email = email;
        _hashedPassword = Hash(password);
    }

    //Method to have a password hasher
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

    //Compare passwords you give it a string and it gaves you a boolean
    public bool ComparePassword(string password)
    {
        string hasdedPassword = Hash(password); 
        
        if( _hashedPassword == hasdedPassword)
        {
            return true;
        }

        return false;
    }

    //Change the user name 
    public bool ChangeUserName(string name, string lastName)
    {
        
        if ( name.Trim() == string.Empty || lastName.Trim() == string.Empty)
        {
            return false;
        }
        
        _name = name;
        _lastName = lastName;

        return true;
    }
    
    //Change the user email
    public bool ChangeEmail(string email)
    {
        if (email.Trim() == string.Empty)
        {
            return false;
        }
        
        _email = email;
        return true;
    }
    
    // Returns the user fullname
    public string GetFullName()
    {
        return $"{_name} {_lastName}";
    }
    
    
    
    
    
}