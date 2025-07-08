using Microsoft.EntityFrameworkCore;

public class CodeNestContext : DbContext
{
    public CodeNestContext(DbContextOptions<CodeNestContext> options) : base(options) { }

    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<Assignment> Assignments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // İlişkiler ve ek konfigürasyonlar burada tanımlanabilir
    }
} 