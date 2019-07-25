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
                Default = "62CFE46E-EDD7-44A8-B3BF-08D7101708E7"
            });
        }
    }
}