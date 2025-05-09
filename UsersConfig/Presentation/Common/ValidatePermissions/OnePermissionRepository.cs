using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using vortexUserConfig.UsersConfig.Infrastructure;
using vortexUserConfig.UsersConfig.Infrastructure.Entities;

namespace vortexUserConfig.UsersConfig.Presentation.Common.ValidatePermissions;

public class OnePermissionRepository : Infrastructure.Entities.Permissions
{
    //Values for the constructor
    private readonly UserConfigDbContext _context;

    //
    // OnePermissionRepository class is a heredit this atributes from the UserEntity
    //
    // public Guid Id = Guid.NewGuid();
    // public string Title = "";
    // public string Description = "";
    // public Guid CreatedBy;
    // public DateTime CreatedAt = DateTime.Now;
    // public Guid? UpdatedBy = null;
    // public DateTime? UpdatedAt = null;
    // public bool IsDisable = false;
    // public Guid? DeletedBy = null;
    // public bool IsDeleted = false;

    //Constructor
    public OnePermissionRepository(UserConfigDbContext context)
    {
        _context = context;
    }
    
    //Private method for the init of the permission when it is first created
    private void InitPermission(Guid createdBy, string title, string description)
    {
        this.CreatedBy = createdBy;
        this.Title = title;
        this.Description = description;
    }

    //Private method for the init of the permission when it is pull from the database
    private void InitPermission(
        Guid id, string title, string description,
        Guid createdBy, DateTime createdAt , Guid? updatedBy ,
        DateTime? updatedAt , bool isDisable , Guid? deletedBy , bool isDeleted )
    {
        this.Id = id;
        this.Title = title;
        this.Description = description;
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
        _context.Permissions.Update(this);
        await _context.SaveChangesAsync();
    }
    
    private void SetUpdateBy(Guid updatedBy)
    {
        this.UpdatedBy = updatedBy;
        this.UpdatedAt = DateTime.Now;
    }
    
    public async Task<OnePermissionRepository> Create(Guid userId, string title, string description)
    {
        InitPermission(userId, title, description);
        
        _context.Permissions.Add(this);
        await _context.SaveChangesAsync();
        
        return this;
    }
    
    public async Task<OnePermissionRepository> GetById(Guid id)
    {
        var permission = await _context.Permissions.FirstOrDefaultAsync(p => p.Id == id);

        if (permission is not null)
        {
            InitPermission(
                permission.Id, permission.Title, permission.Description,
                permission. CreatedBy, permission.CreatedAt , permission.UpdatedBy,
                permission.UpdatedAt , permission.IsDisable , permission.DeletedBy , permission.IsDeleted 
                );
        }
        
        return this;
    }
    
    public async Task<OnePermissionRepository> FindBySpecification(Expression<Func<Permissions, bool>> spec)
    {
        IQueryable<Permissions> query = _context.Permissions.Where(spec);

        var permission = await query.FirstOrDefaultAsync();
        
        if( permission is not null )
        {        
            InitPermission(
                permission.Id, permission.Title, permission.Description,
                permission. CreatedBy, permission.CreatedAt , permission.UpdatedBy,
                permission.UpdatedAt , permission.IsDisable , permission.DeletedBy , permission.IsDeleted 
            );
        } 
        
        return this;
    }

    
    public async Task<OnePermissionRepository> ChangeTitle(Guid id, Guid updatedBy, string title, string description)
    {
        this.Title = title;
        SetUpdateBy(updatedBy);
        await SaveAsync();
        
        return this;
    }
    
    public async Task<OnePermissionRepository> ChangeDescription(Guid id, Guid updatedBy, string title, string description)
    {
        
        this.Description = description;
        SetUpdateBy(updatedBy);
        await SaveAsync();
        
        return this;
    }
    
    public async Task<bool> Disable(Guid id, Guid updatedBy)
    {
        
        this.IsDisable = true;
        SetUpdateBy(updatedBy);
        await SaveAsync();
        
        return true;
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
        _context.Permissions.Remove(this);
        await _context.SaveChangesAsync();
        return true;
    }
    
}