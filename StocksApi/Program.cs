using StocksApi.Mappers;
using StocksApi.DAL;
using StocksApi.BAL;
using StocksMicroservice; // generated from the proto file
using StocksApi.Middleware;
using StocksApi.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(); // Enable controllers for this application

builder.Services.AddScoped<FiltersMapper>(); // dependency injection 
builder.Services.AddScoped<StockMapper>();
builder.Services.AddScoped<GrpcStockMapper>();
builder.Services.AddScoped<GrpcStockRequestMapper>();

builder.Services.AddScoped<IStocksBAL, StocksBAL>(); // dependency injection for BAL
builder.Services.AddScoped<IStocksDAL, StocksDAL>(); // dependency injection for DAL

builder.Services.AddScoped<IMakesBAL, MakesBAL>();
builder.Services.AddScoped<IMakesDAL, MakesDAL>();

builder.Services.AddScoped<ICitiesBAL, CitiesBAL>();
builder.Services.AddScoped<ICitiesDAL, CitiesDAL>();

builder.Services.AddScoped<IDbConnectionFactory, MySqlConnectionFactory>();

builder.Services.AddGrpcClient<StocksService.StocksServiceClient>( // StocksServiceClient is generated from the proto file. It is used to communicate with the gRPC service.
    options =>
    {
        options.Address = new Uri("https://localhost:7042"); // Set the address of the gRPC service to connect to. See from launch settings of the microservice
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers(); // Map the routes defined by my controllers to endpoints.

app.Run();
