using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StarWarsAPI.Data;
using StarWarsAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration
    .GetConnectionString("SqlServer");


// Use LocalDB
// 1. In appsettings.json, write a new connection string: Server=(localdb)\\MSSQLLocalDB;Integrated Security=True
// 2. Change the connection string in Program.cs for the new one
// 3. Install the NuGet Microsoft.EntityFrameworkCore.Tools
// 4. Open Tools > NuGet Package Manager > Package Manager Console
// 5. Execute the command Add-Migration InitialCreate
// 6. Execute the command Update-Database
string connectionStringLocalDb = builder.Configuration
    .GetConnectionString("SqlServerLocalDB");


builder.Services.AddDbContext<StarWarsContext>
    (options => options.UseSqlServer(connectionString));
builder.Services.AddTransient<IRepositoryHabitants, RepositoryHabitants>();
builder.Services.AddTransient<IRepositoryPlanets, RepositoryPlanets>();
builder.Services.AddTransient<IRepositorySpecies, RepositorySpecies>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Habitants of Tatooine",
        Description = "System to register the possible rebels in Tatooine",
        Version = "v1"
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(
        url: "/swagger/v1/swagger.json",
        name: "API v1"
        );
    options.RoutePrefix = "";
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
