using MediatR;
using Application.Auth.Dtos;

namespace Application.Auth;
public record LoginCommand(Guid TenantId, LoginRequest Request) : IRequest<AuthResponse>;

