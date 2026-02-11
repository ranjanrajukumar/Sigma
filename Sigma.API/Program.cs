using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Sigma.API.Middleware;
using Sigma.Infrastructure.DI;

using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ---------------- Controllers ----------------
builder.Services.AddControllers();

// ---------------- Infrastructure DI ----------------
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddInfrastructure(builder.Configuration);



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

// ---------------- Middleware ----------------
app.UseMiddleware<GlobalActivityLoggingMiddleware>();

//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
