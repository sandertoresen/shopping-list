using MySql.Data.MySqlClient;
using ShoppingListAPI.Database;
using ShoppingListAPI.models;


var builder = WebApplication.CreateBuilder(args);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "allowAll",
        b =>
        {
            b.AllowAnyOrigin() // Allow requests from any origin
                .AllowAnyMethod() // Allow any HTTP method (GET, POST, etc.)
                .AllowAnyHeader(); // Allow any headers
        });
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    // .AddEnvironmentVariables()
    .Build();

// dependency inject database
builder.Services.AddSingleton<DatabaseCalls>(provider =>
{
    var connectionString = config["ConnectionString"];
    return new DatabaseCalls(connectionString);
});

var app = builder.Build();
// Use CORS policy
app.UseCors("allowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
