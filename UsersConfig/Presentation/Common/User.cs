using System.Linq.Expressions;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using vortexUserConfig.UsersConfig.Infrastructure;
using vortexUserConfig.UsersConfig.Infrastructure.Entities;
using vortexUserConfig.UsersConfig.Presentation.Common.ValidatePermissions;
using vortexUserConfig.UsersConfig.Presentation.Services.Authentication;
using vortexUserConfig.UsersConfig.Presentation.Services.JwtConfig;

namespace vortexUserConfig.UsersConfig.Presentation.Common;

public class User : Infrastructure.Entities.Users
{
    // Variables for the constructor 
    private readonly JwtToken _jwtToken; 
    private readonly PasswordService _passwordService = new PasswordService();
    private readonly UserConfigDbContext _userDbContext;

    //
    // User class is a heredit this atributes from the UserEntity
    //
    // public Guid Id = Guid.NewGuid();
    // public string Name;
    // public string LastName;
    // public string UserName;
    // public string Email;
    // public string Password;
    // public Guid? RoleId;
    // public Roles? Roles;

    //Made to create base user that contains the basic information
    public User(
        JwtToken jwtToken,
        UserConfigDbContext userDbContext)
    {
        //Constructor
        _jwtToken = jwtToken;
        _userDbContext = userDbContext;
    }
    
    //Private method use for the init of a new user
    private void InitUser(
        string name, string lastName, string userName,
        string email, string password, Guid? roleId, Roles? role
    )
    {
        this.Name = name;
        this.LastName = lastName;
        this.UserName = userName;
        this.Email = email;
        this.Password = password;
        this.RoleId = roleId;
        this.Role = role;
    }
    
    //Private method use for the init of a user get from the database
    private void InitUser(
        Guid id, string name, string lastName, string userName,
        string email, string password, Guid? roleId, Roles? role
    )
    {
        this.Id = id;
        this.Name = name;
        this.LastName = lastName;
        this.UserName = userName;
        this.Email = email;
        this.Password = password;
        this.RoleId = roleId;
        this.Role = role;
    }

    private async Task SaveAsync()
    {
        _userDbContext.Users.Update(this);
        await _userDbContext.SaveChangesAsync();
    }

    //Method to create a new user
    public async Task<User> Create(
        string name, string lastName, string userName,
        string email, string password, Guid? roleId, Roles? role
    )
    {
        if (! (MailAddress.TryCreate(email, out var mailAddress)))
        {
            return null;    
        }

        //To create the user 
        InitUser(name, lastName, userName, email, password, roleId, role);
        
        //Save the user
        _userDbContext.Users.Add(this);
        await _userDbContext.SaveChangesAsync();
        return this;
    }
    
    //Method to get a user by its id
    public async Task<string> GetById(Guid id)
    {
        var user =  await _userDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        
        if(user == null)
        {
            return  "User not found";
        }
        
        InitUser(
            user.Id, user.Name, user.LastName, user.UserName,
            user.Email, user.Password, user.RoleId, user.Role!
        );
        
        return "User " + GetFullName() + " found and loaded ";
    }
    
    public async Task<User> FindBySpecification(Expression<Func<Users, bool>> spec)
    {
        IQueryable<Users> query = _userDbContext.Users.Where(spec);

        var user = await query.FirstOrDefaultAsync();
        
        if(user is not null )
        {
            InitUser(user.Id, user.Name, user.LastName, 
                user.UserName, user.Email, user.Password,
                user.RoleId, user.Role!);
        } 
        
        return this;
    }

    //Method to get a JwtToken for the user
    public string GenerateJwtToken()
    {
        return _jwtToken.GenerateToken(this);
    }
    
    //Compare passwords you give it a string and it gaves you a boolean
    public bool ComparePassword(string password)
    {
        string hashdedPassword = _passwordService.Hash(password); 
        
        if( this.Password == hashdedPassword)
        {
            return true;
        }

        return false;
    }

    //Change the user name 
    public async Task<bool> ChangeUserName(string name, string lastName)
    {
        if ( name.Trim() == string.Empty || lastName.Trim() == string.Empty)
        {
            return false;
        }
        
        this.Name = name;
        this.LastName = lastName;

        await SaveAsync();
        
        return true;
    }
    
    //Change the user email
    public async Task<bool> ChangeEmail(string email)
    {
        if (new MailAddress(email).Address != email)
        {
            return false;
        }
        
        this.Email = email;
        await SaveAsync();
        return true;
    }
    
    public async Task<bool> ChangePassword(string password)
    {
        if (password.Trim() == string.Empty)
        {
            return false;
        }
        
        this.Password = _passwordService.Hash(password);
        await SaveAsync();
        return true;
    }
    
    public async Task<bool> ChangeRole(Guid roleId)
    {
        this.RoleId = roleId;
        await SaveAsync();
        return true;
    }
    
    // Returns the user fullname
    public string GetFullName()
    {
        return $"{this.Name} {this.LastName}";
    }
    
    
    
}