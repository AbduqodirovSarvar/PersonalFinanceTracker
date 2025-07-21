using Application.Interfaces;
using Application.Models;
using Application.Models.Common;
using AutoMapper;
using Domain.Enums;
using MediatR;

namespace Application.Features.Users.Commands.Update
{
    public class UpdateUserCommandHandler(
        IUserRepositoy userRepository,
        ICurrentUserService currentUserService,
        IMapper mapper) : IRequestHandler<UpdateUserCommand, Result<UserViewModel>>
    {
        private readonly IUserRepositoy _userRepository = userRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<UserViewModel>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(x => x.Id == request.Id);

            if (user != null && _currentUserService.UserId != user.Id && _currentUserService.Role != Role.SuperAdmin.ToString())
                return Result<UserViewModel>.Fail("You are not authorized to update this user.");

            if (user is null)
                return Result<UserViewModel>.Fail("User not found");

            user.UserName = request.UserName;
            user.Email = request.Email;

            await _userRepository.UpdateAsync(user);
            var viewModel = _mapper.Map<UserViewModel>(user);

            return Result<UserViewModel>.Ok("User successfully updated.", viewModel);
        }
    }
}
