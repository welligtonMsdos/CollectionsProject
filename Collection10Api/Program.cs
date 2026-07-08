using Collection10Api.Application.Interfaces;
using Collection10Api.Application.Validators.Concerts;
using Collection10Api.Domain.Interfaces;
using Collection10Api.Infrastructure.Data;
using Collection10Api.Infrastructure.Filters;
using Collection10Api.Infrastructure.Middleware;
using Collection10Api.Infrastructure.Repositories;
using Collection10Api.Infrastructure.Service;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System.Data;
using System.Text;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins("http://13.59.37.186", "http://localhost:4200")
               .WithMethods("GET", "POST", "PUT", "DELETE")
               .WithHeaders("Content-Type", "Authorization")
               .AllowCredentials();
    });
});

builder.Services.AddValidatorsFromAssemblyContaining<ConcertCreateValidator>();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidatorFilter>();
});

builder.Services.AddOpenApi();

var secret = Environment.GetEnvironmentVariable("JwtSettings__Key");

if (string.IsNullOrEmpty(secret) || secret.Length < 32)
    throw new InvalidOperationException("JWT key is missing or too short (minimum 32 characters).");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
            ValidateIssuer = true,
            ValidIssuer = "http://13.59.37.186:5011",           
            ValidateAudience = false,
            ValidateLifetime = true
        };
    });

builder.Services.AddAuthorization();

var connectionString = builder.Configuration.GetConnectionString("CollectionConnection");

if (string.IsNullOrEmpty(connectionString))
    throw new InvalidOperationException("Connection string 'CollectionConnection' is missing in configuration.");

builder.Services.AddDbContext<CollectionContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddScoped<IDbConnection>(_ => new NpgsqlConnection(connectionString));

builder.Services.AddScoped<IConcertDapperRepository, ConcertDapperRepository>();
builder.Services.AddScoped<IConcertEFRepository, ConcertEFRepository>();
builder.Services.AddScoped<IConcertService, ConcertService>();

builder.Services.AddScoped<IVinylDapperRepository, VinylDapperRepository>();
builder.Services.AddScoped<IVinylEFRepository, VinylEFRepository>();
builder.Services.AddScoped<IVinylService, VinylService>();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapOpenApi();

app.MapScalarApiReference(options =>
{
    options.Title = "Api Authentication";
    options.Theme = ScalarTheme.BluePlanet;
    options.DefaultHttpClient = new(ScalarTarget.JavaScript, ScalarClient.HttpClient);
    options.CustomCss = "";
    options.ShowSidebar = true;
    options.AddPreferredSecuritySchemes("Bearer")
           .AddHttpAuthentication("Bearer", auth =>
           {
               auth.Token = "your-bearer-token";
           });
});

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
