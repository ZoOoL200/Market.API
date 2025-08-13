using Market.Infrastucture.Extension;
using Market.Infrastucture.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMarketInfrastructure(builder.Configuration);

var app = builder.Build();
// seed the database with initial data
var scop = app.Services.CreateScope();
var service = scop.ServiceProvider;
;
await service.GetRequiredService<ICountryKeySeeder>().SeedAsync();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
