// MathTestSystem.API/Models/UploadExamModel.cs
using Microsoft.AspNetCore.Http;

namespace MathTestSystem.API.Models
{
    public class UploadExamModel
    {
        /// <summary>
        /// The XML file containing exam data.
        /// </summary>
        public IFormFile XmlFile { get; set; }
    }
}
