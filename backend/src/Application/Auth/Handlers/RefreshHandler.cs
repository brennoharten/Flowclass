using MediatR;
using Application.Auth.Dtos;
using Application.Common.Interfaces;
using Domain.Entities;
using Application.Common.Interfaces.Repositories;

namespace Application.Auth;

public class RefreshHandler : IRequestHandler<RefreshCommand, AuthResponse>
{
    private readonly IUserRepository _users;
    private readonly IJwtTokenService _jwt;

    public RefreshHandler(IUserRepository users, IJwtTokenService jwt)
        => (_users, _jwt) = (users, jwt);

    public async Task<AuthResponse> Handle(RefreshCommand cmd, CancellationToken ct)
    {
        var token = await _users.GetRefreshTokenAsync(cmd.Request.RefreshToken, ct)
                    ?? throw new InvalidOperationException("Refresh inválido.");

        if (token.IsRevoked || token.ExpiresAt < DateTime.UtcNow)
            throw new InvalidOperationException("Refresh expirado.");

        var user = token.User!; // configure Include no repo
        var access = _jwt.CreateAccessToken(user);

        // (opcional) rotacionar refresh
        await _users.InvalidateRefreshTokenAsync(token, ct);
        var newStr = _jwt.CreateRefreshToken();
        await _users.AddRefreshTokenAsync(new RefreshToken(user.Id, newStr, DateTime.UtcNow.AddDays(30)), ct);
        await _users.SaveChangesAsync(ct);

        return new AuthResponse(user.Id, user.Name, user.Email, access, newStr);
    }
}

