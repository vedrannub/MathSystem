using MathTestSystem.Application.DTOs;
using MediatR;

namespace MathTestSystem.Application.Queries
{
    public class GetStudentExamsQuery : IRequest<List<ExamDto>>
    {
        public string StudentId { get; }

        public GetStudentExamsQuery(string studentId)
        {
            StudentId = studentId;
        }
    }
}
