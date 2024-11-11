using Microsoft.EntityFrameworkCore;

namespace MathTestSystem.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Teacher> Teachers { get; set; }
        DbSet<Student> Students { get; set; }
        DbSet<Exam> Exams { get; set; }
        DbSet<ExamTask> Tasks { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
