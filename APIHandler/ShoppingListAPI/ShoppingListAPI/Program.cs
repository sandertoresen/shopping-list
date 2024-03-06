using MySql.Data.MySqlClient;
using ShoppingListAPI.Database;
using ShoppingListAPI.models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

// dependency inject database
builder.Services.AddSingleton<DatabaseCalls>(provider =>
{
    var connectionString = config["ConnectionString"];
    return new DatabaseCalls(connectionString);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
