using System.Reflection;
using Microsoft.EntityFrameworkCore;
using vortexUserConfig.UsersConfig.Infrastructure.Entities;
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
        
        modelBuilder.Entity<Users>()
            .HasOne(p => p.Role);
        modelBuilder.Entity<Users>()
            .HasOne(p => p.Role)
            .WithMany()
            .HasForeignKey(p => p.RoleId);

        modelBuilder.Entity<Roles>().HasKey(p => p.Id);

        modelBuilder.Entity<Roles>()
            .HasMany(e => e.Permissions)
            .WithMany(e => e.Roles)
            .UsingEntity<RolePermissions>(
                l => l.HasOne<Permissions>().WithMany().HasForeignKey(e => e.PermissionId),
                r => r.HasOne<Roles>().WithMany().HasForeignKey(e => e.RoleId)
            ); 

        modelBuilder.Entity<Permissions>().HasKey(p => p.Id);
        
        modelBuilder.Entity<Permissions>()
            .HasMany(e => e.Roles)
            .WithMany(e => e.Permissions)
            .UsingEntity<RolePermissions>(
                l => l.HasOne<Roles>().WithMany().HasForeignKey(e => e.RoleId),
                r => r.HasOne<Permissions>().WithMany().HasForeignKey(e => e.PermissionId)
            ); 
        
        modelBuilder.Entity<RolePermissions>().HasKey(p => p.Id);
        
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
 
    }

}
