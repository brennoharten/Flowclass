using MediatR;
using Domain.Entities;
using Application.Users.Dtos;

namespace Application.Users.Queries
{
    public record GetCurrentUserQuery(Guid TenantId, string Email) : IRequest<UserDto>;
}
