using MathTestSystem.Application.DTOs;
using MathTestSystem.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MathTestSystem.Application.Queries
{
    public class GetStudentExamsQueryHandler : IRequestHandler<GetStudentExamsQuery, List<ExamDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetStudentExamsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ExamDto>> Handle(GetStudentExamsQuery request, CancellationToken cancellationToken)
        {
            var exams = await _context.Exams
                .Include(e => e.Tasks)
                .Where(e => e.StudentID == request.StudentId)
                .ToListAsync(cancellationToken);

            return exams.Select(exam => new ExamDto
            {
                Id = exam.ID,
                Tasks = exam.Tasks.Select(task => new TaskDto
                {
                    Id = task.ID,
                    TaskText = task.TaskText,
                    IsCorrect = task.IsCorrect,
                    CorrectResult = (double)task.CorrectResult,
                    StudentResult = (double)task.StudentResult
                }).ToList()
            }).ToList();
        }
    }
}
