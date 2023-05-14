using DollarConverterServer;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<ConverterService>();

app.Run();
