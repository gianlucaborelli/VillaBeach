using Api.CrossCutting.Configuration;
using Api.CrossCutting.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigureFirebase.ConfigureFirebaseAuthentication(builder.Services);
ConfigureService.ConfigureDependenciesService(builder.Services);
ConfigureRepository.ConfigureDependenciesRepository(builder.Services);
ConfigureMapper.ConfigureMapperService(builder.Services);

builder.Services.AddScoped<ModelBindingFailureFilter>();

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

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();