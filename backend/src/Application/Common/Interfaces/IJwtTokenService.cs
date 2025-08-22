using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IJwtTokenService
    {
        string CreateAccessToken(User user);
        string CreateRefreshToken(); // string randômica
    }
}