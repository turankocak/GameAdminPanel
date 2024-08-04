using Microsoft.EntityFrameworkCore;
using AdminPanelBackend.Models;

namespace AdminPanelBackend.Data 
{
    public class YourDbContext : DbContext
    {
        public YourDbContext(DbContextOptions<YourDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
