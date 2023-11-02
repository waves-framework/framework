// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Waves.Framework;
using Waves.Framework.Sandbox.Console;

var builder = WavesApplicationBuilder.CreateBuilder();

// logging
builder.Logging = loggingBuilder =>
{
    loggingBuilder.AddConsole();
};

// configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

var app = builder.Build();

var sandboxService = app.Services.GetInstance<SandboxService>();
var logger = app.Services.GetInstance<ILogger<Program>>();
var random = sandboxService.GetRandom();
logger.LogInformation($"Random: {random}");

Console.ReadLine();