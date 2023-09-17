using Api.CrossCutting.Configuration;
using Api.CrossCutting.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureFirebaseAuthentication();
builder.Services.ConfigureMapperService();
builder.Services.ConfigureDependenciesRepository();
builder.Services.ConfigureDependenciesService();
builder.Services.AddScoped<ModelBindingFailureFilter>();

builder.Services.AddLogging();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseProblemDetailsExceptionHandler();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();