using MathTestSystem.API.Models; 
using MathTestSystem.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MathTestSystem.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ExamsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExamsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Upload([FromForm] UploadExamModel model)
        {
            var xmlFile = model.XmlFile;

            if (xmlFile == null || xmlFile.Length == 0)
                return BadRequest("File is empty");

            using (var stream = new System.IO.MemoryStream())
            {
                await xmlFile.CopyToAsync(stream);
                stream.Position = 0;

                var command = new UploadExamsCommand(stream);
                var result = await _mediator.Send(command);

                if (result)
                    return Ok("File processed successfully");
                else
                    return StatusCode(500, "An error occurred while processing the file");
            }
        }
    }
}
