using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using vortexUserConfig.UsersConfig.Infrastructure;
using vortexUserConfig.UsersConfig.Infrastructure.Entities;
namespace vortexUserConfig.UsersConfig.Presentation.Common.ListQuery;

public class ListPermissionRepository
{
    private readonly UserConfigDbContext _context;
    
    public ListPermissionRepository(UserConfigDbContext context)
    {
        _context = context;
    }

    public async Task<List<Permissions>> GetAll(UserConfigDbContext context)
    {
        return await _context.Permissions.ToListAsync(); 
    }
    
    public async Task<List<Permissions>> FindBySpecification(Expression<Func<Permissions, bool>> spec)
    {
        IQueryable<Permissions> query = _context.Permissions.Where(spec);

        var roles = await query.ToListAsync();
        
        return roles;
    }
}