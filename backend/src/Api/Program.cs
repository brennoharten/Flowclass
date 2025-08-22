using Infrastructure.Persistence;
using Infrastructure.Tenancy;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Npgsql.EntityFrameworkCore.PostgreSQL; // necessário para UseSnakeCaseNamingConvention
using System.Text;
using Application;

var builder = WebApplication.CreateBuilder(args);

// ======================
// Serviços
// ======================

// TenantProvider: 1 por requisição
builder.Services.AddScoped<ITenantProvider, TenantProvider>();

// DbContext
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        npg => npg.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
    );
    opt.UseSnakeCaseNamingConvention(); // cuidado: requer pacote Npgsql.EntityFrameworkCore.PostgreSQL.NamingConventions
});

// JWT Auth
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
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            )
        };
    });

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(IApplicationAssemblyMarker).Assembly));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ======================
// Pipeline
// ======================

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

// Middleware Tenant: pega X-Tenant-Id do header
app.Use(async (ctx, next) =>
{
    var tenantProvider = ctx.RequestServices.GetRequiredService<ITenantProvider>();

    var header = ctx.Request.Headers["X-Tenant-Id"].FirstOrDefault();
    if (Guid.TryParse(header, out var tid))
        tenantProvider.Set(tid); // usa a instância correta

    await next();
});

app.MapControllers();

app.Run();
