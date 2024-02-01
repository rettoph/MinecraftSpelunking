var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("LettuceEncrypt:Enabled => " + builder.Configuration["LettuceEncrypt:Enabled"]);
if (builder.Configuration["LettuceEncrypt:Enabled"] == "true")
{
    builder.Services.AddLettuceEncrypt();
}

var app = builder.Build();

app.MapGet("/", () => "Hello World!!!!");

app.Run();
