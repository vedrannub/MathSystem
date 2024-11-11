using MathTestSystem.API.Attributes;
using MathTestSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MathTestSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiKey] 
    public class IntegrationController : ControllerBase
    {
        private readonly IMathProcessor _mathProcessor;

        public IntegrationController(IMathProcessor mathProcessor)
        {
            _mathProcessor = mathProcessor;
        }

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
                return BadRequest(new { error = $"Failed to evaluate expression: {ex.Message}" });
            }
        }
    }

  
    public class CalculationRequest
    {
        public string Expression { get; set; }
    }

    public class CalculationResponse
    {
        public string Expression { get; set; }
        public double Result { get; set; }
    }
}
