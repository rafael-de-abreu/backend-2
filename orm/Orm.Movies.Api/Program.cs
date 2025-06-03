using Microsoft.EntityFrameworkCore;
using Orm.Movies.Api.Services;
using Orm.Movies.Dapper;
using Orm.Movies.DataModels;
using Orm.Movies.Ef;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<MovieService>();
builder.Services.AddScoped<IMovieRepository, MovieEfRepository>();
//builder.Services.AddScoped<IMovieRepository, MovieDapperRepository>();
builder.Services.AddScoped<IDbConnection>(sp =>
{
    var connectionString = builder.Configuration["ConnectionStrings:Postgres"];
    return new Npgsql.NpgsqlConnection(connectionString);
});
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration["ConnectionStrings:Postgres"]);
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

app.Run();
