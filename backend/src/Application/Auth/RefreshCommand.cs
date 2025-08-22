using MediatR;
using Application.Auth.Dtos;

namespace Application.Auth;
public record RefreshCommand(Guid TenantId, RefreshRequest Request) : IRequest<AuthResponse>;

