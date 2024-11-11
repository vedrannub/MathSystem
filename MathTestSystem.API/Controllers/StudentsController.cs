using MathTestSystem.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MathTestSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{studentId}/exams")]
        public async Task<IActionResult> GetStudentExams(string studentId)
        {
            var query = new GetStudentExamsQuery(studentId);
            var exams = await _mediator.Send(query);

            if (exams == null || exams.Count == 0)
                return NotFound("Student or exams not found");

            return Ok(exams);
        }
    }
}
