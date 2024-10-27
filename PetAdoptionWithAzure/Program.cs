using PetAdoptionWithAzure.Data;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env file
Env.Load();

// Get the user and password from the environment variables
var dbUser = Environment.GetEnvironmentVariable("DB_USER");
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

// Get the base connection string from appsettings.json
var baseConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Replace the placeholders with the actual user and password
var connectionString = baseConnectionString?
    .Replace("User ID=", $"User ID={dbUser}")
    .Replace("Password=", $"Password={dbPassword}");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<AnimalContext>(options =>
    options.UseSqlServer(connectionString));

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

app.UseAuthorization();

app.MapControllers();

app.Run();
