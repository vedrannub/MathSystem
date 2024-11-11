using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MathTestSystem.API.Swagger
{
    public class FileUploadOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Check if the operation has any parameters of type IFormFile
            var fileParameters = context.MethodInfo.GetParameters()
                .Where(p => p.ParameterType == typeof(IFormFile))
                .ToList();

            if (fileParameters.Count == 0)
                return;

            // Remove existing parameters
            operation.Parameters.Clear();

            // Add a request body with multipart/form-data content type
            operation.RequestBody = new OpenApiRequestBody
            {
                Content =
                {
                    ["multipart/form-data"] = new OpenApiMediaType
                    {
                        Schema = GenerateSchema(fileParameters)
                    }
                }
            };
        }

        private OpenApiSchema GenerateSchema(List<System.Reflection.ParameterInfo> fileParameters)
        {
            var properties = new Dictionary<string, OpenApiSchema>();

            foreach (var fileParam in fileParameters)
            {
                properties.Add(fileParam.Name, new OpenApiSchema
                {
                    Type = "string",
                    Format = "binary"
                });
            }

            return new OpenApiSchema
            {
                Type = "object",
                Properties = properties,
                Required = (ISet<string>)fileParameters.Select(p => p.Name).ToList()
            };
        }
    }
}
