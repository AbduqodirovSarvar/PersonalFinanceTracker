using Workers;
using Workers.Extentions;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddWorkers(builder.Configuration);

var host = builder.Build();
host.Run();
