using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace Infrastructure.Identity
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Email =>
            _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value
            ?? throw new Exception("Email claim not found");

        public Guid TenantId =>
            Guid.Parse(
                _httpContextAccessor.HttpContext?.User.FindFirst("tenantId")?.Value
                ?? throw new Exception("TenantId claim not found")
            );

        public string Role =>
            _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value
            ?? throw new Exception("Role claim not found");
    }
}
