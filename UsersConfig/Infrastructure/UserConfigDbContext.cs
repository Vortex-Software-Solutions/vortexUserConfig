using System.Reflection;
using Microsoft.EntityFrameworkCore;
using vortexUserConfig.UsersConfig.Infrastructure.Entities;
using vortexUserConfig.UsersConfig.Presentation.Common;

namespace vortexUserConfig.UsersConfig.Infrastructure;

public class UserConfigDbContext: DbContext
{
    public UserConfigDbContext(DbContextOptions<UserConfigDbContext> options) : base(options)
    {
    }
    
    public virtual DbSet<Users> Users { get; set; }
    public DbSet<Roles> Roles { get; set; }
    public DbSet<Permissions> Permissions { get; set; }
    public DbSet<RolePermissions> RolePermissions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Users>()
            .HasKey(p => p.Id);
        
        modelBuilder.Entity<Roles>().HasKey(p => p.Id);
        
        modelBuilder.Entity<Permissions>().HasKey(p => p.Id);
        
        modelBuilder.Entity<RolePermissions>().HasKey(p => p.Id);
        
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}