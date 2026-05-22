using DataAccessLayer;
using Microsoft.OpenApi;
using PSBackend.BAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PSBackend.DAL;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddControllers();

// Swagger Configuration
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
  options.SwaggerDoc("v1", new OpenApiInfo
  {
    Title = "PSBackend API",
    Version = "v1",
    Description = "Backend APIs for PSBackend Project"
  });
});

// Dependency Injection Setup
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IPSDAL, PSDAL>();
builder.Services.AddScoped<SqlDataClient>();

builder.Services.AddScoped<ITempleRepository, TempleRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICatalogRepository, CatalogRepository>();
builder.Services.AddScoped<ITokenRepository , TokenRepository>();



// Configure CORS
builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowAngular",
      policy =>
      {
        policy.WithOrigins(
              "https://icy-moss-07b6d0200.7.azurestaticapps.net", "http://localhost:4200"
          )
          .AllowAnyHeader()
          .AllowAnyMethod();
      });
});



// -------------------- JWT AUTHENTICATION --------------------
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],

        IssuerSigningKey = new SymmetricSecurityKey(
              Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key is missing.")))
      };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.

  app.UseSwagger();
  app.UseSwaggerUI(options =>
  {
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "PSBackend API V1");
    options.RoutePrefix = string.Empty; // Opens Swagger at root URL
  });


app.UseHttpsRedirection();

app.UseCors("AllowAngular");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
