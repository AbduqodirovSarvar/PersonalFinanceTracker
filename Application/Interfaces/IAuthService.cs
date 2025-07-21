using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        (string Token, User User) Authenticate(string userName, string password);
        Task<User> RegisterAsync(string userName, string password, string email);
        Task<User?> ChangePasswordAsync(Guid userId, string oldPassword, string newPassword);
        Task<User?> ChangeRoleAsync(Guid userId, Role newRole);
        Task<User?> ResetPasswordAsync(Guid userId, string newPassword, string conformPassword, int confirmationCode = 12345);
    }
}
