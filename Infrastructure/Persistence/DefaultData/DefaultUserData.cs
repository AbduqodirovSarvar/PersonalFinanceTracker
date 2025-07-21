using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Persistence.DefaultData
{
    public class DefaultUserData
    {
        private static List<User>? _users;

        private DefaultUserData() { }

        public static List<User> Users(IHashService hashService)
        {
            _users = [
                new User(){
                    Email = "defaultuser@gmail.com",
                    UserName = "defaultuser",
                    Role = Role.SuperAdmin,
                    PasswordHash = hashService.Hash("12345")
                }
            ];

            return _users;
        }
    }
}
