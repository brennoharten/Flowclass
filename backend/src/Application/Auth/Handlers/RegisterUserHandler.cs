using MediatR;
using Application.Auth.Dtos;
using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Auth;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, AuthResponse>
{
    private readonly IUserRepository _users;
    private readonly IPasswordHasher _hasher;
    private readonly IJwtTokenService _jwt;

    public RegisterUserHandler(IUserRepository users, IPasswordHasher hasher, IJwtTokenService jwt)
        => (_users, _hasher, _jwt) = (users, hasher, jwt);

    public async Task<AuthResponse> Handle(RegisterUserCommand cmd, CancellationToken ct)
    {
        var req = cmd.Request;

        var exists = await _users.GetByEmailAsync(cmd.TenantId, req.Email, ct);
        if (exists is not null) throw new InvalidOperationException("Email já cadastrado.");

        var hash = _hasher.Hash(req.Password);
        var role = (UserRole)req.Role;

        var user = new User(cmd.TenantId, req.Email, hash, req.Name, role);
        await _users.AddAsync(user, ct);

        var access = _jwt.CreateAccessToken(user);
        var refreshStr = _jwt.CreateRefreshToken();
        var refresh = new RefreshToken(user.Id, refreshStr, DateTime.UtcNow.AddDays(30));
        await _users.AddRefreshTokenAsync(refresh, ct);

        await _users.SaveChangesAsync(ct);

        return new AuthResponse(user.Id, user.Name, user.Email, access, refreshStr);
    }
}

