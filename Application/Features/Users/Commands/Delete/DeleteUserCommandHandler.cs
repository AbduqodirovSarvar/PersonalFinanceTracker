using Application.Interfaces;
using Application.Models;
using Application.Models.Common;
using Domain.Enums;
using MediatR;

namespace Application.Features.Users.Commands.Delete
{
    public class DeleteUserCommandHandler(
        IUserRepositoy userRepository,
        ICurrentUserService currentUserService)
        : IRequestHandler<DeleteUserCommand, Result<UserViewModel>>
    {
        private readonly IUserRepositoy _userRepository = userRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<Result<UserViewModel>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(u => u.Id == request.Id);

            if (user != null && _currentUserService.UserId != user.Id && _currentUserService.Role != Role.SuperAdmin.ToString())
                return Result<UserViewModel>.Fail("You are not authorized to delete this user.");

            if (user == null)
                return Result<UserViewModel>.Fail("User not found.");

            await _userRepository.DeleteAsync(user);

            return Result<UserViewModel>.Ok("User successfully deleted.");
        }
    }
}
