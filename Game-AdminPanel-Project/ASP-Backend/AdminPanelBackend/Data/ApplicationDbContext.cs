using Microsoft.EntityFrameworkCore;
using AdminPanelBackend.Models; 

namespace AdminPanelBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Building> Buildings { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("User_Table");
            modelBuilder.Entity<Building>().ToTable("Building_Table"); 
        }
    }
}
