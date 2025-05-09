
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using vortexUserConfig.UsersConfig.Infrastructure;
using vortexUserConfig.UsersConfig.Infrastructure.Entities;

namespace vortexUserConfig.UsersConfig.Presentation.Common.ListQuery;

public class ListUsersRepository
{
    private readonly UserConfigDbContext _context;
    
    public ListUsersRepository(UserConfigDbContext context)
    {
        _context = context;
    }

    public async Task<List<Users>> GetAll(UserConfigDbContext context)
    {
        return await _context.Users.ToListAsync(); 
    }
    
    public async Task<List<Users>> FindBySpecification(Expression<Func<Users, bool>> spec)
    {
        IQueryable<Users> query = _context.Users.Where(spec);

        var users = await query.ToListAsync();
        
        return users;
    }
    
}