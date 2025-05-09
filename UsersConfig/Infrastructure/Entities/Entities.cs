using vortexUserConfig.UsersConfig.Presentation.Common.ValidatePermissions;

namespace vortexUserConfig.UsersConfig.Infrastructure.Entities;

public class Users
{
    //OneUserRepository personal data
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string LastName { get; set; }
    
    //OneUserRepository config 
    public string UserName { get; set; }
    public string Email { get; set; }
    
    //OneUserRepository password
    public string Password { get; set; }
    
    //OneUserRepository role
    //El usuario puede no tener un rol
    public Guid? RoleId { get; set; }
    public Roles? Role { get; set; }
}

public class Roles
{
    //Paramaters
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "";
    
    //Relations
    public List<Guid> PermissionId { get; set; } = [];
    public List<Permissions> Permissions { get; set; } = [];
    
    //Data for management
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public Guid? UpdatedBy { get; set; } = null;
    public DateTime? UpdatedAt { get; set; } = null;
    public bool IsDisable { get; set; } = false;
    public Guid? DeletedBy { get; set; } = null;
    public bool IsDeleted { get; set; } = false;
}

public class Permissions
{
    //Values for the class
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = "";
    public string Description{ get; set; } = "";
    
    //Relations
    public List<Roles> Roles { get; set; } = [];

    
    //Data for management
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public Guid? UpdatedBy { get; set; } = null;
    public DateTime? UpdatedAt { get; set; } = null;
    public bool IsDisable { get; set; } = false;
    public Guid? DeletedBy { get; set; } = null;
    public bool IsDeleted { get; set; } = false;
}

public class RolePermissions
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid RoleId { get; set; }
    public Roles Roles { get; set; }
    public Guid PermissionId { get; set; }
    public Permissions Permissions { get; set; }
}

public class Sessions
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
}