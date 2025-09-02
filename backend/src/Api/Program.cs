using Infrastructure.Persistence;
using Infrastructure.Tenancy;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Application;
using Application.Common.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Security;
using Infrastructure.Mail;
using Infrastructure.Identity;
using Application.Common.Interfaces.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ======================
// Serviços
// ======================

// TenantProvider: 1 por requisição
builder.Services.AddScoped<ITenantProvider, TenantProvider>();

// Repos
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILessonRepository, LessonRepository>();
builder.Services.AddScoped<IClassroomRepository, ClassroomRepository>();
// no bloco de services
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContext, UserContext>();


// Serviços
builder.Services.AddSingleton<IPasswordHasher, BcryptPasswordHasher>();
builder.Services.AddSingleton<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IEmailSender, SmtpEmailSender>();

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

// Middleware Tenant
app.Use(async (ctx, next) =>
{
    var tenantProvider = ctx.RequestServices.GetRequiredService<ITenantProvider>();

    var header = ctx.Request.Headers["X-Tenant-Id"].FirstOrDefault();
    if (Guid.TryParse(header, out var tid))
        tenantProvider.Set(tid); // usa a instância correta

    await next();
});

app.MapControllers();

// ===== Seed antes de rodar a aplicação =====
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
    await SeedDataAsync(db, hasher);
}

app.Run();

// ===== Método Seed =====
async Task SeedDataAsync(AppDbContext db, IPasswordHasher hasher)
{
    if (!await db.Tenants.AnyAsync())
    {
        var tenant = new Tenant("Default Tenant");
        await db.Tenants.AddAsync(tenant);

        var admin = new User(
            tenant.Id,
            "admin@admin.com",
            hasher.Hash("1234"),
            "Admin",
            UserRole.Admin
        );
        await db.Users.AddAsync(admin);

        await db.SaveChangesAsync();
    }
}
