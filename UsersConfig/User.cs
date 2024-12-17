
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using vortexUserConfig.UsersConfig.Authentication;
using vortexUserConfig.UsersConfig.JwtConfig;
using vortexUserConfig.UsersConfig.ValidatePermissions;

namespace vortexUserConfig.UsersConfig;

public class User
{
    private readonly JwtToken _jwtToken; 

    private readonly PasswordService _passwordService = new PasswordService();
    //User Id 
    public Guid id = Guid.NewGuid();
    
    //User personal data
    public string name;
    public string lastName;
  
    //User config 
    public string userName;
    public string email;
    
    //User password
    public string password;
    
    //User role
    public Guid roleId;
    public Role? role;
    
    //Made to create base user that contains the basic information
    public User(
        string name, string lastName, string userName, 
        string email, string password, Role role, JwtToken jwtToken
        )
    {

        if (! (MailAddress.TryCreate(email, out var mailAddress)))
        {
            throw new Exception("Invalid email");
        }
        
        //Constructor
        _jwtToken = jwtToken;
        
        //To create the user 
        this.name = name;
        this.lastName = lastName;
        this.userName = userName;
        this.email = mailAddress.ToString();
        this.role = role;
        this.password = PasswordService.Hash(password);
    }
    
    public User GetUser()
    {
        return this;
    }
    
    public string GenerateJwtToken()
    {
        return _jwtToken.GenerateToken(this);
    }
    
    //Compare passwords you give it a string and it gaves you a boolean
    public bool ComparePassword(string password)
    {
        string hasdedPassword = (password); 
        
        if( this.password == hasdedPassword)
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
        
        this.name = name;
        this.lastName = lastName;

        return true;
    }
    
    //Change the user email
    public bool ChangeEmail(string email)
    {
        if (email.Trim() == string.Empty)
        {
            return false;
        }
        
        this.email = email;
        return true;
    }
    
    // Returns the user fullname
    public string GetFullName()
    {
        return $"{this.name} {this.lastName}";
    }
    
    
    
    
}