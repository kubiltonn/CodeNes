using Microsoft.EntityFrameworkCore;

namespace code_MVC.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
    }
} 