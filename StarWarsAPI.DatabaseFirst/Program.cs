using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("SqlServer");


builder.Services.AddControllers();
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

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    options.RoutePrefix = "";
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
