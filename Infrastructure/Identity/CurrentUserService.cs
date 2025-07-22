using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace Application.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public Guid? UserId { get; }
        public string? Email { get; }
        public string? Username { get; }
        public string? Role { get; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var userClaims = httpContextAccessor.HttpContext?.User?.Claims;
            if (userClaims == null) return;

            var idClaim = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (idClaim != null && Guid.TryParse(idClaim.Value, out var id))
                UserId = id;

            Email = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            Username = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            Role = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
        }
    }
}
