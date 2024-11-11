using MathTestSystem.API.Models;
using MediatR;

namespace MathTestSystem.Application.Queries
{
    public class GetAllStudentsQuery : IRequest<List<StudentViewModel>>
    {
    }
}
