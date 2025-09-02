using MediatR;
using Application.Auth.Dtos;
using Application.Common.Interfaces;
using Domain.Entities;
using Application.Common.Interfaces.Repositories;

namespace Application.Auth;

public class LoginHandler : IRequestHandler<LoginCommand, AuthResponse>
{
    private readonly IUserRepository _users;
    private readonly IPasswordHasher _hasher;
    private readonly IJwtTokenService _jwt;

    public LoginHandler(IUserRepository users, IPasswordHasher hasher, IJwtTokenService jwt)
        => (_users, _hasher, _jwt) = (users, hasher, jwt);

    public async Task<AuthResponse> Handle(LoginCommand cmd, CancellationToken ct)
    {
        var req = cmd.Request;
        var user = await _users.GetByEmailAsync(cmd.TenantId, req.Email, ct)
                   ?? throw new InvalidOperationException("Credenciais inválidas.");

        if (!_hasher.Verify(user.PasswordHash, req.Password))
            throw new InvalidOperationException("Credenciais inválidas.");

        var access = _jwt.CreateAccessToken(user);
        var refreshStr = _jwt.CreateRefreshToken();
        var refresh = new RefreshToken(user.Id, refreshStr, DateTime.UtcNow.AddDays(30));
        await _users.AddRefreshTokenAsync(refresh, ct);
        await _users.SaveChangesAsync(ct);

        return new AuthResponse(user.Id, user.Name, user.Email, access, refreshStr);
    }
}

