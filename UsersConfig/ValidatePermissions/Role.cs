namespace vortexUserConfig.UsersConfig.ValidatePermissions;

public class Role
{
    public Guid id = Guid.NewGuid();
    public string name;
    public List<Permission> permissions;
    
    //Constructor to create a role with a name and a list of permissions 
    public Role(string name, List<Permission> permissions)
    {
        this.name = name;
        this.permissions = permissions;
    } 
    
    public bool CheckForPermission(string permission)
    {
        
        return permissions.Any(p => p.title == permission);
    }
    
    public
    
}

