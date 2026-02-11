using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Sigma.API.Middleware;
using Sigma.Application.Interfaces.Utilities;
using Sigma.Domain.Entities.Utilities;
using Sigma.Infrastructure.DI;
using Sigma.Infrastructure.Persistence;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// ---------------- Controllers ----------------
builder.Services.AddControllers();

// ---------------- DapperContext (MUST BE BEFORE Build) ----------------
builder.Services.AddSingleton<DapperContext>();

// ---------------- Infrastructure DI ----------------
builder.Services.AddInfrastructure(builder.Configuration);

// ---------------- Validation Logging ----------------
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var logger = context.HttpContext.RequestServices
            .GetRequiredService<IGlobalActivityLogService>();

        var errors = context.ModelState
            .Where(x => x.Value?.Errors.Count > 0)
            .Select(x => new
            {
                Field = x.Key,
                Errors = x.Value!.Errors.Select(e => e.ErrorMessage)
            });

        var errorJson = JsonSerializer.Serialize(errors);

        // ⚠ NEVER use .Wait() in ASP.NET Core
        _ = logger.LogAsync(new GlobalActivityLog
        {
            Level = "Warning",
            Service = "Sigma.API",
            Source = context.HttpContext.Request.Path,
            Message = "Validation Failed",
            Request = errorJson,
            TraceId = context.HttpContext.TraceIdentifier
        });

        return new BadRequestObjectResult(context.ModelState);
    };
});

// ---------------- JWT Authentication ----------------
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();

// ---------------- Swagger ----------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Sigma School ERP API",
        Version = "v1"
    });
});

var app = builder.Build();

// ---------------- Mongo Initialization ----------------
using (var scope = app.Services.CreateScope())
{
    var mongoContext = scope.ServiceProvider
        .GetRequiredService<MongoDbContext>();

    await mongoContext.InitializeAsync();
}

// ---------------- Middleware ----------------

// 1️⃣ Global Exception FIRST
app.UseMiddleware<GlobalExceptionMiddleware>();

// 2️⃣ Request Logging
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
