using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MathTestSystem.API.Swagger
{
    public class FileUploadOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var fileParameters = context.MethodInfo.GetParameters()
                .Where(p => p.ParameterType == typeof(IFormFile))
                .ToList();

            if (fileParameters.Count == 0)
                return;

            operation.Parameters.Clear();

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
