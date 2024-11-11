using MathTestSystem.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MathTestSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeachersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TeachersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("students")]
        public async Task<IActionResult> GetStudents()
        {
            var query = new GetAllStudentsQuery();
            var students = await _mediator.Send(query);
            return Ok(students);
        }
    }
}
