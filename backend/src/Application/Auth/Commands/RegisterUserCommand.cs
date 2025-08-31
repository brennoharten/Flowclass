using MediatR;
using Application.Auth.Dtos;

namespace Application.Auth;
public record RegisterUserCommand(Guid TenantId, RegisterRequest Request) : IRequest<AuthResponse>;

