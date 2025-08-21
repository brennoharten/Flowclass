csharp
CopiarEditar
using Infrastructure.Persistence;
using Infrastructure.Tenancy;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddScoped<ITenantProvider, TenantProvider>(); // per-request
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
        npg => npg.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
    opt.UseSnakeCaseNamingConvention();
});

// Auth JWT (simplificado)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }

app.UseAuthentication();
app.UseAuthorization();

// Middleware de Tenant (dev: lê do header X-Tenant-Id)
app.Use(async (ctx, next) =>
{
    var header = ctx.Request.Headers["X-Tenant-Id"].FirstOrDefault();
    if (Guid.TryParse(header, out var tid))
        TenantProvider.Set(tid);
    await next();
});

app.MapControllers();

app.Run();

