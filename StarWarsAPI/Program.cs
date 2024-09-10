using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StarWarsAPI.Data;
using StarWarsAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder.Configuration
    .GetConnectionString("SqlServer");
builder.Services.AddDbContext<StarWarsContext>
    (options => options.UseSqlServer(connectionString));
builder.Services.AddTransient<IRepositoryStarWars, RepositoryStarWars>();


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
