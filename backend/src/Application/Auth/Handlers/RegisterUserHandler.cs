using MediatR;
using Application.Auth.Dtos;
using Application.Common.Interfaces;
using Domain.Entities;
using Application.Common.Interfaces.Repositories;


namespace Application.Auth;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, AuthResponse>
{
    private readonly IUserRepository _users;
    private readonly ITenantRepository _tenants;
    private readonly IJwtTokenService _jwt;
    private readonly IPasswordHasher _hasher;

    public RegisterUserHandler(IUserRepository users, ITenantRepository tenants, IJwtTokenService jwt, IPasswordHasher hasher)
        => (_users, _tenants, _jwt, _hasher) = (users, tenants, jwt, hasher);

    public async Task<AuthResponse> Handle(RegisterUserCommand cmd, CancellationToken ct)
    {
        var req = cmd.Request;

        Guid tenantId;

        if (req.Role == (int)UserRole.Teacher)
        {
            var tenant = new Tenant(req.AcademyName);
            await _tenants.AddAsync(tenant, ct);
            tenantId = tenant.Id;
        }

        else
        {
            tenantId = req.TenantId ?? throw new Exception("TenantId obrigatório para aluno");
        }

        var user = new User(
            tenantId,
            req.Email,
            _hasher.Hash(req.Password),
            req.Name,
            (UserRole)req.Role
        );


        await _users.AddAsync(user, ct);

        var token = _jwt.CreateAccessToken(user);

        return new AuthResponse(user.Id, user.Name, user.Email, req.Role.ToString(), token);
    }
}



