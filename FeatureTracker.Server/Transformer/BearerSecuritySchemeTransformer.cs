using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace FeatureTracker.Server;

internal sealed class BearerSecuritySchemeTransformer(IAuthenticationSchemeProvider authenticationSchemeProvider)
    : IOpenApiDocumentTransformer
{
    public async Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context,
        CancellationToken cancellationToken)
    {
        #region Old
        //    var authenticationSchemes = await authenticationSchemeProvider.GetAllSchemesAsync();
        //    if (authenticationSchemes.Any(authScheme => authScheme.Name == "Bearer"))
        //    {
        //        var requirements = new Dictionary<string, OpenApiSecurityScheme>
        //        {
        //            ["Bearer"] = new()
        //            {
        //                Type = SecuritySchemeType.Http,
        //                Scheme = "Bearer",
        //                In = ParameterLocation.Header,
        //                BearerFormat = "Json Web Token"
        //            }
        //        };
        //        document.Components ??= new OpenApiComponents();
        //        document.Components.SecuritySchemes = requirements;

        //        foreach (var operation in document.Paths.Values.SelectMany(path => path.Operations))
        //            operation.Value.Security.Add(new OpenApiSecurityRequirement
        //            {
        //                [new OpenApiSecurityScheme { Reference = new OpenApiReference { Id = "Bearer", Type = ReferenceType.SecurityScheme } }] =
        //                    Array.Empty<string>()
        //            });
        //    }
        //}
        #endregion

        document.Components = new OpenApiComponents
        {
            Headers = new Dictionary<string, OpenApiHeader>
            {
                { "Authorization", new OpenApiHeader { Required = true } }
            },

            SecuritySchemes = new Dictionary<string, OpenApiSecurityScheme>
            {
                ["Bearer"] = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    In = ParameterLocation.Header,
                    BearerFormat = "JWT"
                }
            }
        };

        document.Servers.Add(new OpenApiServer
        {
            Url = "https://localhost:8081",
            Description = "Localhost server"
        });
    }
}
