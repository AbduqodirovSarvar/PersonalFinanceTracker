using Domain.Enums;
using System.Security.Claims;

namespace Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Claim[] claims);
    }
}
