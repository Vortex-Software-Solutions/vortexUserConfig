namespace vortexUserConfig.UsersConfig.ValidatePermissions;

public class Permission
{
    public Guid id = Guid.NewGuid();
    public string title;
    public string description;
    
    public Permission(string title, string description)
    {
        this.title = title;
        this.description = description;
    }
    
    
}