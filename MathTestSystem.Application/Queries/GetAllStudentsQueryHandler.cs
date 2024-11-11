using MathTestSystem.API.Models;
using MathTestSystem.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MathTestSystem.Application.Queries
{
    public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, List<StudentViewModel>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllStudentsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<StudentViewModel>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var students = await _context.Students
                .Include(s => s.Exams)
                    .ThenInclude(e => e.Tasks)
                .ToListAsync(cancellationToken);

            var studentViewModels = students.Select(s => new StudentViewModel
            {
                ID = s.ID,
                Exams = s.Exams.Select(e => new ExamViewModel
                {
                    ID = e.ID,
                    Tasks = e.Tasks.Select(t => new ExamTaskViewModel
                    {
                        ID = t.ID,
                        TaskText = t.TaskText,
                        CorrectResult = t.CorrectResult,
                        StudentResult = t.StudentResult,
                        IsCorrect = t.IsCorrect
                    }).ToList()
                }).ToList()
            }).ToList();

            return studentViewModels;
        }
    }
}
