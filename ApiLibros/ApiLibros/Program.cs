using ApiLibros.Areas.Identity.Data;
using ApiLibros.Data;
using ApiLibros.Mappings;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApiLibrosContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddDefaultIdentity<ApiLibrosUser>(options => 
    options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApiLibrosContext>();

// Add services to the container.
builder.Services.AddCors(options => options.AddPolicy("AllowWebApp",
    builder => builder.AllowAnyOrigin()
    .AllowAnyHeader()
        .AllowAnyMethod()));

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    Console.WriteLine("Desarrollo");
}

// Configure the HTTP request pipeline.
app.UseCors("AllowWebApp");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
