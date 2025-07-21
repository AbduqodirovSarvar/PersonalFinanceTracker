using Domain.Enums;

namespace Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Guid userId, string email, Role role, string userName);
    }
}
