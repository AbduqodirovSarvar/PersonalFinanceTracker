using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Identity
{
    public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public Guid? UserId => Guid.TryParse(_httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == "uid")?.Value, out var id) ? id : null;

        public string? Email => _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

        public string? Username => _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

        public string? Role => _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
    }
}
