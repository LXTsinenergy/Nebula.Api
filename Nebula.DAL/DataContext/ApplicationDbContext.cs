using Microsoft.EntityFrameworkCore;

namespace Nebula.DAL.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) => Database.EnsureCreated();    
    }
}
