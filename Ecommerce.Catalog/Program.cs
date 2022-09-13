using Ecommerce.Catalog.Data;
using Ecommerce.Catalog.Messaging;
using Ecommerce.Infraestructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); ;
builder.Services.AddScoped<ICatalogClient, CatalogClient>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddIdGeneratorClient(builder.Configuration);

builder.Services.AddDbContext<CatalogContext>(options => {
    var connString = builder.Configuration.GetConnectionString("Catalog");
    options.UseMySql(connString, new MySqlServerVersion(new Version(5, 7)))
    .LogTo(Console.WriteLine, LogLevel.Information)
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors();
});

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

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<CatalogContext>();
dbContext.Database.Migrate();

app.Run();

