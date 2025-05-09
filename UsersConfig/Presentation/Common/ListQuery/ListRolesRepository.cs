using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using vortexUserConfig.UsersConfig.Infrastructure;
using vortexUserConfig.UsersConfig.Infrastructure.Entities;

namespace vortexUserConfig.UsersConfig.Presentation.Common.ListQuery;

public class ListRolesRepository
{
    private readonly UserConfigDbContext _context;
    
    public ListRolesRepository(UserConfigDbContext context)
    {
        _context = context;
    }

    public async Task<List<Roles>> GetAll(UserConfigDbContext context)
    {
        return await _context.Roles.ToListAsync(); 
    }
    
    public async Task<List<Roles>> FindBySpecification(Expression<Func<Roles, bool>> spec)
    {
        IQueryable<Roles> query = _context.Roles.Where(spec);

        var roles = await query.ToListAsync();
        
        return roles;
    }
}