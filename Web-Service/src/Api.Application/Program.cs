using Api.CrossCutting.Configuration;
using Api.CrossCutting.DependencyInjection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container.
builder.Services.ConfigureAuthentication();
builder.Services.ConfigureMapperService();
builder.Services.ConfigureDependenciesRepository();
builder.Services.ConfigureDependenciesService();
builder.Services.AddScoped<ModelBindingFailureFilter>();

builder.Services.AddLogging();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();
builder.Services.AddHttpContextAccessor();

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
app.UseSerilogRequestLogging();

app.UseProblemDetailsExceptionHandler();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();