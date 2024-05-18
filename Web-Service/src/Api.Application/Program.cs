using Api.CrossCutting.Communication.Settings;
using Api.CrossCutting.Configuration;
using Api.CrossCutting.DependencyInjection;
using MediatR;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container.
builder.Services.ConfigureAuthentication();
builder.Services.ConfigureMapperService();
builder.Services.ConfigureDependenciesRepository();
builder.Services.ConfigureDependenciesService();
builder.Services.RegisterServices();
builder.Services.AddScoped<ModelBindingFailureFilter>();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddLogging();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();
builder.Services.AddHttpContextAccessor();

builder.Services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("/swagger/v1/swagger.json", "WebService V1");
    });
}

app.Use(async (context, next) =>
    {
        if (context.Request.Path == "/" && app.Environment.IsDevelopment())
        {
            context.Response.Redirect("/swagger/index.html");
            return;
        }

        await next();
    });

app.UseSerilogRequestLogging();
app.UseStaticFiles();

app.UseProblemDetailsExceptionHandler();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();