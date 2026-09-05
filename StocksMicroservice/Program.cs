using StocksMicroservice.Services;
using StocksMicroservice.DAL;
using StocksMicroservice.Database;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc(); // Enable gRPC infrastructure

builder.Services.AddScoped<IStocksDAL, StocksDAL>();
builder.Services.AddScoped<IDbConnectionFactory, MySqlConnectionFactory>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<StocksGrpcService>(); // Expose our StocksService as a gRPC endpoint
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
