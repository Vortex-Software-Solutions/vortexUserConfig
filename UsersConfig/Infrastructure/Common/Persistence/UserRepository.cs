

using Microsoft.EntityFrameworkCore;

namespace vortexUserConfig.UsersConfig.Infrastructure.Common.Persistence;

public class UserRepository 
{
    private readonly UserConfigDbContext _context;

    public UserRepository(UserConfigDbContext context)
    {
        _context = context;
    }
    
    
    
}