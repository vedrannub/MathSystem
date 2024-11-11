using MathTestSystem.API.Attributes;
using MathTestSystem.Application.Interfaces;
using MathTestSystem.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MathTestSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiKey] // Ensures only authorized clients can access these endpoints
    public class IntegrationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMathProcessor _mathProcessor;

        public IntegrationController(IMediator mediator, IMathProcessor mathProcessor)
        {
            _mediator = mediator;
            _mathProcessor = mathProcessor;
        }

        /// <summary>
        /// Retrieves exams for a specific student.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        /// <returns>A list of exams for the student.</returns>
        [HttpGet("Students/{studentId}/Exams")]
        public async Task<IActionResult> GetStudentExams(string studentId)
        {
            var query = new GetStudentExamsQuery(studentId);
            var exams = await _mediator.Send(query);

            if (exams == null || exams.Count == 0)
                return NotFound("Student or exams not found");

            return Ok(exams);
        }

        /// <summary>
        /// Evaluates a mathematical expression and returns the result.
        /// </summary>
        /// <param name="request">The calculation request containing the expression.</param>
        /// <returns>The result of the calculation.</returns>
        [HttpPost("calculate")]
        public IActionResult Calculate([FromBody] CalculationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Expression))
            {
                return BadRequest(new { error = "Expression is required." });
            }

            try
            {
                double result = _mathProcessor.EvaluateExpression(request.Expression);

                var response = new CalculationResponse
                {
                    Expression = request.Expression,
                    Result = result
                };

                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                // Log the exception if necessary
                return BadRequest(new { error = $"Failed to evaluate expression: {ex.Message}" });
            }
        }
    }

    /// <summary>
    /// Represents a request to perform a calculation.
    /// </summary>
    public class CalculationRequest
    {
        public string Expression { get; set; }
    }

    /// <summary>
    /// Represents the result of a calculation.
    /// </summary>
    public class CalculationResponse
    {
        public string Expression { get; set; }
        public double Result { get; set; }
    }
}
