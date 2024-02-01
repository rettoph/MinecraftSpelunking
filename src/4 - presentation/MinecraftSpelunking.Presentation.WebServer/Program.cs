var builder = WebApplication.CreateBuilder(args);

if (builder.Configuration["LettuceEncrypt:Enabled"] == "true")
{
    builder.Services.AddLettuceEncrypt();
}

var app = builder.Build();

app.MapGet("/", () => "Hello World!!!!");

app.Run();
