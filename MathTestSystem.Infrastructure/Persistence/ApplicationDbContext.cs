using MathTestSystem.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MathTestSystem.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamTask> Tasks { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exam>()
                .HasKey(e => new { e.StudentID, e.ID });

            modelBuilder.Entity<Exam>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Exams)
                .HasForeignKey(e => e.StudentID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ExamTask>()
                .HasKey(t => new { t.StudentID, t.ExamID, t.ID });

            modelBuilder.Entity<ExamTask>()
                .HasOne(t => t.Exam)
                .WithMany(e => e.Tasks)
                .HasForeignKey(t => new { t.StudentID, t.ExamID })
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
