using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

using Application.Services;
using Domain.Repositories;
using Infrastructure.Data.Jwt;
using Infrastructure.Jwt;
using Infrastructure.Repositories;
using RealEstateApi.src.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

/* Config DbContext */
builder.Services.AddDbContext<RealEstateDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null
        );
    })
);
/* Config DbContext */

/* Inject dependences */
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<PropertyService>();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});
builder.Services.AddAntiforgery(options => 
{
    options.Cookie.Name = "X-CSRF-TOKEN";
});
/* Inject dependences */

/* Add Authentication */
var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is missing");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtKey)),
        ClockSkew = TimeSpan.Zero
    };

    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            var result = System.Text.Json.JsonSerializer.Serialize(new { message = "Authentication failed" });
            return context.Response.WriteAsync(result);
        }
    };
});
/* Add Authentication */

var app = builder.Build();


/* Add Middleware */
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<PerformanceMiddleware>();
/* Add Middleware */

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
