using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthService(
        IUserRepositoy userRepository,
        ITokenService tokenService,
        IHashService hashService,
        ICurrentUserService currentUserService
        ) : IAuthService
    {
        private readonly IUserRepositoy _userRepository = userRepository;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IHashService _hashService = hashService;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<(string Token, User User)> Authenticate(string login, string password)
        {
            var user = await _userRepository.GetAsync(x => x.UserName == login || x.Email == login);

            if (user is null)
                throw new Exception("User not found.");

            if (!_hashService.Verify(password, user.PasswordHash))
                throw new Exception("Invalid password.");

            var token = _tokenService.GenerateToken(user.Id, user.Email, user.Role, user.UserName);

            return (token, user);
        }

        public async Task<User> RegisterAsync(string userName, string password, string email)
        {
            var existingUser = _userRepository.Query(u => u.UserName.ToLower() == userName.ToLower() || u.Email.ToLower() == email.ToLower()).FirstOrDefault();

            if (existingUser != null)
                throw new Exception("User with this username or email already exists.");

            var hashedPassword = _hashService.Hash(password);

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                UserName = userName,
                Email = email,
                PasswordHash = hashedPassword,
                Role = Role.User
            };

            return await _userRepository.CreateAsync(newUser);
        }

        public async Task<User?> ChangePasswordAsync(Guid userId, string oldPassword, string newPassword)
        {
            if(_currentUserService.UserId != userId && _currentUserService.Role != Role.Admin.ToString() && _currentUserService.Role != Role.SuperAdmin.ToString())
                throw new Exception("You do not have permission to change this user's password.");

            var user = await _userRepository.GetAsync(u => u.Id == userId);
            if (user is null) return null;

            if (!_hashService.Verify(oldPassword, user.PasswordHash))
                throw new Exception("Old password is incorrect.");

            user.PasswordHash = _hashService.Hash(newPassword);
            return await _userRepository.UpdateAsync(user);
        }

        public async Task<User?> ChangeRoleAsync(Guid userId, Role newRole)
        {
            var lowUserRoles = new[] { Role.Moderator.ToString(), Role.User.ToString(), Role.None.ToString() };
            if (_currentUserService.Role != Role.SuperAdmin.ToString() && !(_currentUserService.Role == Role.Admin.ToString() && lowUserRoles.Contains(_currentUserService.Role)))
                throw new Exception("You do not have permission to change this user's password.");
            var user = await _userRepository.GetAsync(u => u.Id == userId);
            if (user is null) return null;

            user.Role = newRole;
            return await _userRepository.UpdateAsync(user);
        }

        public async Task<User?> ResetPasswordAsync(Guid userId, string newPassword, string conformPassword, int confirmationCode = 12345)
        {
            if(confirmationCode != 12345)
                throw new Exception("Invalid confirmation code.");

            if (newPassword != conformPassword)
                throw new Exception("Passwords do not match.");

            var user = await _userRepository.GetAsync(u => u.Id == userId);
            if (user is null) return null;

            user.PasswordHash = _hashService.Hash(newPassword);
            return await _userRepository.UpdateAsync(user);
        }
    }
}
