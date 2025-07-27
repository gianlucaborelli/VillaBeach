using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.CrossCutting.Configuration
{
    public static class SwaggerConfigurations
    {
        public static void AddSwaggerConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(options =>
            {
                var xmlFile = "Application.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "villabeach_webservice",
                    Version = "v1",
                    Contact = new OpenApiContact()
                    {
                        Name = "Gianluca Borelli",
                        Url = new Uri("https://github.com/gianlucaborelli"),
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "MIT",
                        Url = new Uri("http://opensource.org/licenses/MIT"),
                    },
                    Description = "This is the API documentation for My API"
                }
                );

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme.\r\n\r\n Enter 'Bearer'[space] and then your token in the text input below. \r\n\r\nExample: \"Bearer 12345abcdef\"",
                });                
                options.OperationFilter<AuthResponsesOperationFilter>();
            });
        }

        public class AuthResponsesOperationFilter : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                var authAttributes = context.MethodInfo.DeclaringType!.GetCustomAttributes(true)
                    .Union(context.MethodInfo.GetCustomAttributes(true))
                    .OfType<AuthorizeAttribute>();

                if (authAttributes.Any())
                {
                    var securityRequirement = new OpenApiSecurityRequirement()
                    {                        
                        {                            
                            // Put here you own security scheme, this one is an example
                            new OpenApiSecurityScheme
                            {
                                
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,
                            },
                            new List<string>()
                        }
                    };
                    operation.Security = new List<OpenApiSecurityRequirement> { securityRequirement };
                }
            }
        }

        public static void UseSwaggerConfiguration(this WebApplication app)
        {
            if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
            {
                app.UseSwagger();
                app.UseSwaggerUI(opt =>
                {
                    opt.SwaggerEndpoint("/swagger/v1/swagger.json", "villabeach_webservice V1");
                });
            }
            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/swagger" && (app.Environment.IsDevelopment() || app.Environment.IsStaging()))
                {
                    context.Response.Redirect("/swagger/index.html");
                    return;
                }
                await next();
            });
        }
    }
}