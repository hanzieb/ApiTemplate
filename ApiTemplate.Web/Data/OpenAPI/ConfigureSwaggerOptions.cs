using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ApiTemplate.Web.Data.OpenAPI
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }

            // Add token authentication option to pass bearer token
            //options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            //{
            //    In = ParameterLocation.Header,
            //    Description = "Please enter token",
            //    Name = "Authorization",
            //    Type = SecuritySchemeType.Http,
            //    BearerFormat = "JWT",
            //    Scheme = "bearer"
            //});

            //// Add security scheme
            //options.AddSecurityRequirement(new OpenApiSecurityRequirement
            //{
            //    {
            //        new OpenApiSecurityScheme
            //        {
            //            Reference = new OpenApiReference
            //            {
            //                Type = ReferenceType.SecurityScheme,
            //                Id = "Bearer"
            //            }
            //        },
            //        new string[] { }
            //    }
            //});
        }

        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription apiVersionDescription)
        {
            var info = new OpenApiInfo
            {
                Title = "API Versioning",
                Version = apiVersionDescription.ApiVersion.ToString(),
                Description = "Swagger document for API Versioning.",
            };

            // Add deprecated API description
            if (apiVersionDescription.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }
}