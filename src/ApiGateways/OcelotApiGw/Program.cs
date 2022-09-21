using Microsoft.Extensions.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(p =>
{
    p.AddConsole();
    p.AddConfiguration(builder.Configuration.GetSection("Logging"));
    p.AddDebug();
}) ;
var configBuilder = new ConfigurationBuilder();
configBuilder.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json",true,true);
var config= configBuilder.Build();
builder.Configuration.AddConfiguration(config);
builder.Services.AddOcelot();
var app = builder.Build();

app.UseRouting();
app.UseOcelot();
app.Run();
