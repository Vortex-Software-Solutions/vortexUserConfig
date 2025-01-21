using Microsoft.EntityFrameworkCore;
using vortexUserConfig.UsersConfig.Infrastructure;
using vortexUserConfig.UsersConfig.Infrastructure.Entities;

namespace vortexUserConfig.UsersConfig.Presentation.Common.ValidatePermissions;

public class Role : Infrastructure.Entities.Roles
{
    //Data for the constructor
    private readonly UserConfigDbContext _context;
 
    //
    // Roles class is a heredit this atributes from the UserEntity
    //
    // public Guid Id = Guid.NewGuid();
    // public string Name = "";
    // public List<Guid> PermissionId = [];
    // public List<Permission> Permissions = [];
    // public Guid CreatedBy;
    // public DateTime CreatedAt = DateTime.Now;
    // public Guid? UpdatedBy = null;
    // public DateTime? UpdatedAt = null;
    // public bool IsDisable = false;
    // public Guid? DeletedBy = null;
    // public bool IsDeleted = false;

    //Constructor para que se use con permisos
    public Role(UserConfigDbContext context)
    {
        _context = context;
    }

    //Private method for the init of the permission when it is first created
    private void InitPermission(Guid createdBy, string name, List<Permissions> permissions)
    {
        this.CreatedBy = createdBy;
        this.Name = name;
        this.Permissions = permissions;
    }

    //Private method for the init of the permission when it is pull from the database
    private void InitPermission(
        Guid id, string name, List<Permissions> permissions,
        Guid createdBy, DateTime createdAt, Guid? updatedBy,
        DateTime? updatedAt, bool isDisable, Guid? deletedBy, bool isDeleted)
    {
        this.Id = id;
        this.Name = name;
        this.Permissions = permissions;
        this.CreatedBy = createdBy;
        this.CreatedAt = createdAt;
        this.UpdatedBy = updatedBy;
        this.UpdatedAt = updatedAt;
        this.IsDisable = isDisable;
        this.DeletedBy = deletedBy;
        this.IsDisable = isDeleted;
    }
    
    private async Task SaveAsync()
    {
        _context.Roles.Update(this);
        await _context.SaveChangesAsync();
    }
    
    private void SetUpdateBy(Guid updatedBy)
    {
        this.UpdatedBy = updatedBy;
        this.UpdatedAt = DateTime.Now;
    }

    public async Task<Role> Create(Guid createdBy, string name, List<Permissions> permissions)
    {
        InitPermission(createdBy, name, permissions);
        _context.Roles.Add(this);
        await _context.SaveChangesAsync();
        return this;
    }

    public async Task<Role> GetById(Guid id)
    {
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);

        if (role == null)
        {
            return new Role(_context) ;
        }

        InitPermission(
            role.Id, role.Name, role.Permissions,
            role.CreatedBy, role.CreatedAt, role.UpdatedBy,
            role.UpdatedAt, role.IsDisable, role.DeletedBy, role.IsDeleted
        );

        return this;
    }
    
    public async Task<Role> ChangeName(Guid updatedBy, string name)
    {
        this.Name = name;
        SetUpdateBy(updatedBy);
        await SaveAsync();
        return this;
    }
    
    public async Task<Role> ChangePermissions(Guid updatedBy, List<Guid> permissionsId)
    {
        this.PermissionId = permissionsId;
        SetUpdateBy(updatedBy);
        await SaveAsync();
        return this;
    } 
    
    public async Task<Role> Disable(Guid updatedBy)
    {
        this.IsDisable = true;
        SetUpdateBy(updatedBy);
        await SaveAsync();
        return this;
    }
    
    public async Task<bool> SoftDelete(Guid id, Guid deletedBy)
    {
        this.DeletedBy = deletedBy;
        this.IsDeleted = true;
        await SaveAsync();
        return true;
        
    } 
    
    public async Task<bool> HardDelete(Guid id)
    {
        _context.Roles.Remove(this);
        await _context.SaveChangesAsync();
        return true;
        
    }



}

