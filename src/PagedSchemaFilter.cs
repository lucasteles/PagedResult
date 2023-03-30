namespace PagedResult.Swagger;

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

/// <summary>
/// Swagger schema filter for paged results
/// </summary>
public class PagedSchemaFilter : ISchemaFilter
{
    /// <inheritdoc />
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var type = context.Type;
        if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(Paged<>))
            return;
        var argumentType = type.GetGenericArguments().First();
        if (context.SchemaRepository.Schemas.ContainsKey(argumentType.Name))
            return;
        var argumentSchema = context.SchemaGenerator
            .GenerateSchema(argumentType, context.SchemaRepository);
        context.SchemaRepository.AddDefinition(argumentType.Name, argumentSchema);
    }
}
