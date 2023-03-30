namespace PagedResult.Swagger;

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

/// <summary>
/// Swagger schema filter for paged results
/// </summary>
public class PagedSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var type = context.Type;
        if (!type.IsGenericType ||
            type.GetGenericTypeDefinition() != typeof(PagedResult.Paged<>))
            return;
        var argumentType = type.GetGenericArguments().First();
        var argumentSchema = context.SchemaGenerator
            .GenerateSchema(argumentType, context.SchemaRepository);
        if (context.SchemaRepository.Schemas.ContainsKey(argumentSchema.Reference.Id))
            return;
        context.SchemaRepository.AddDefinition(argumentType.Name, argumentSchema);
    }
}
