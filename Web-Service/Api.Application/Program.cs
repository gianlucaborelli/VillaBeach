using CrossCutting.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddExtensions();

var app = builder.Build();

app.AddUsings();
app.Run();
