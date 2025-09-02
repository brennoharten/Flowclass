using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;
    public UserRepository(AppDbContext db) => _db = db;

    public Task<User?> GetByEmailAsync(Guid tenantId, string email, CancellationToken ct)
        => _db.Users.FirstOrDefaultAsync(u => u.TenantId == tenantId && u.Email == email, ct);

    public async Task AddAsync(User user, CancellationToken ct) => await _db.Users.AddAsync(user, ct);

    public async Task AddRefreshTokenAsync(RefreshToken token, CancellationToken ct)
        => await _db.RefreshTokens.AddAsync(token, ct);

    public Task<RefreshToken?> GetRefreshTokenAsync(string token, CancellationToken ct)
        => _db.RefreshTokens.Include(r => r.User).FirstOrDefaultAsync(r => r.Token == token, ct);

    public Task InvalidateRefreshTokenAsync(RefreshToken token, CancellationToken ct)
    {
        token.Revoke();
        _db.RefreshTokens.Update(token); // marca alteração para salvar depois
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken ct) => _db.SaveChangesAsync(ct);
}
