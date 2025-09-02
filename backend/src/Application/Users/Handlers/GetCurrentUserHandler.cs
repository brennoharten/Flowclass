using MediatR;
using Application.Users.Dtos;
using Application.Common.Interfaces.Repositories;
using Application.Users.Queries;
using Domain.Entities;

namespace Application.Users.Handlers
{
    public class GetCurrentUserHandler : IRequestHandler<GetCurrentUserQuery, UserDto>
    {
        private readonly IUserRepository _users;

        public GetCurrentUserHandler(IUserRepository users)
            => _users = users;

        public async Task<UserDto> Handle(GetCurrentUserQuery query, CancellationToken ct)
        {
            var user = await _users.GetByEmailAsync(query.TenantId, query.Email, ct)
                ?? throw new Exception("User not found");

            return new UserDto(user.Id, user.Name, user.Email, user.Role.ToString());
        }
    }
}
