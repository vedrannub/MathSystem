using MediatR;
using System.Collections.Generic;
using MathTestSystem.Application.DTOs;

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
