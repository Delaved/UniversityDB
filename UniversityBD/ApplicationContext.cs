using Microsoft.EntityFrameworkCore;


namespace UniversityDB
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<Specialization> Specializations { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<University> Universities { get; set; } = null!;
        public DbSet<Connection_SpecializationsWithCourse> Connection_SpecializationsWithCourses { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { 
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Connection_SpecializationsWithCourse>()
                .HasOne(c => c.specialization)
                .WithMany(c => c.connection_SpecializationsWithCourses)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Connection_SpecializationsWithCourse>()
                .HasOne(c => c.course)
                .WithMany(c => c.connection_SpecializationsWithCourses)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
