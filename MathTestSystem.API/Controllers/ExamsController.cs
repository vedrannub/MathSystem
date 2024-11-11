// MathTestSystem.API/Controllers/ExamsController.cs
using Microsoft.AspNetCore.Mvc;
using MediatR;
using MathTestSystem.Application.Commands;
using MathTestSystem.API.Models; // Include the namespace for UploadExamModel
using System.Threading.Tasks;

namespace MathTestSystem.API.Controllers
{
    /// <summary>
    /// Controller for managing exams.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ExamsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExamsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Uploads an XML file containing exam data.
        /// </summary>
        /// <param name="model">The upload model containing the XML file.</param>
        /// <returns>A status message.</returns>
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
