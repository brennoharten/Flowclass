using MediatR;
using Application.Auth.Dtos;

namespace Application.Auth;
public record RegisterUserCommand(RegisterRequest Request) : IRequest<AuthResponse>;

