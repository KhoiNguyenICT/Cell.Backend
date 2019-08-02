using System.Collections.Generic;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Cell.Common.Filters
{
    public class SwaggerHeaderFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<IParameter>();

            operation.Parameters.Add(new NonBodyParameter
            {
                Name = "Session",
                In = "header",
                Type = "string",
                Required = true
            });
            operation.Parameters.Add(new NonBodyParameter
            {
                Name = "Account",
                In = "header",
                Type = "string",
                Required = true,
                Default = "60886CC0-5566-4B48-8B2A-E9818CFCB5D8"
            });
        }
    }
}