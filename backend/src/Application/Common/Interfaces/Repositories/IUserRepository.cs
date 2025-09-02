using Domain.Entities;

namespace Application.Common.Interfaces.Repositories;
public interface IUserRepository
{
    Task<User?> GetByEmailAsync(Guid tenantId, string email, CancellationToken ct);
    Task AddAsync(User user, CancellationToken ct);
    Task AddRefreshTokenAsync(RefreshToken token, CancellationToken ct);
    Task<RefreshToken?> GetRefreshTokenAsync(string token, CancellationToken ct);
    Task InvalidateRefreshTokenAsync(RefreshToken token, CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
}

