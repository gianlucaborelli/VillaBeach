using Api.CrossCutting.Communication.Settings;
using Api.CrossCutting.Configuration;
using Api.CrossCutting.DependencyInjection;
using Api.CrossCutting.Identity.Extensions;
using Api.CrossCutting.Identity.JWT.Settings;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddRazorPages();
builder.Services.ConfigureMapperService();
builder.ConfigureDependenciesRepository();
builder.Services.ConfigureDependenciesService();
builder.Services.RegisterServices();
builder.Services.AddScoped<ModelBindingFailureFilter>();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddLogging();
builder.Services.AddControllers();
builder.AddAuthenticationConfiguration();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();
builder.Services.AddHttpContextAccessor();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("/swagger/v1/swagger.json", "WebService V1");
    });
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
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

app.UseSerilogRequestLogging();
app.UseStaticFiles();

app.UseProblemDetailsExceptionHandler();
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();

