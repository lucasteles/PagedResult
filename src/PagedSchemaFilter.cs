namespace PagedResult.Swagger;

using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

/// <summary>
/// Swagger schema filter for paged results
/// </summary>
public class PagedSchemaFilter : ISchemaFilter
{
    /// <inheritdoc />
    public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
    {
        var type = context.Type;
        if (!type.IsGenericType ||
            type.GetGenericTypeDefinition() != typeof(Paged<>))
            return;
        var argumentType = type.GetGenericArguments()[0];
        context.SchemaGenerator.GenerateSchema(argumentType, context.SchemaRepository);
    }
}
