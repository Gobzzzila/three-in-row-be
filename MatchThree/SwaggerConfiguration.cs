using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MatchThree.API;

internal class SwaggerConfiguration
{
    internal class AcceptLanguageHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            if (operation.Parameters.Any(x => x.Name == "Accept-Language"))
                return;

            var parameter = new OpenApiParameter
            {
                Name = "Accept-Language",
                Description = "Language preference for the response.",
                In = ParameterLocation.Header,
                AllowEmptyValue = true,
                Schema = new OpenApiSchema()
                {
                    Default = new OpenApiString("en-US"),
                    Enum = new List<IOpenApiAny>
                    {
                        new OpenApiString("en-US"),
                        new OpenApiString("ru-RU")
                    }
                }
            };

            operation.Parameters.Add(parameter);
        }
    }
}