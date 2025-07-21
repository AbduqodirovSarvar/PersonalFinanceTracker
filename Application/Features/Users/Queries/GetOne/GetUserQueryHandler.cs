using Application.Interfaces;
using Application.Models;
using Application.Models.Common;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Queries.GetOne
{
    public class GetUserQueryHandler(
        IUserRepositoy userRepository,
        IMapper mapper)
        : IRequestHandler<GetUserQuery, Result<UserViewModel>>
    {
        private readonly IUserRepositoy _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<UserViewModel>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            User? user = null;

            if (request.UserId.HasValue)
            {
                user = await _userRepository.GetAsync(u => u.Id == request.UserId.Value);
                return user != null
                    ? Result<UserViewModel>.Ok("User found by ID.", _mapper.Map<UserViewModel>(user))
                    : Result<UserViewModel>.Fail("User not found by ID.");
            }

            if (!string.IsNullOrWhiteSpace(request.UserName))
            {
                user = await _userRepository.GetAsync(u => u.UserName == request.UserName);
                return user != null
                    ? Result<UserViewModel>.Ok("User found by username.", _mapper.Map<UserViewModel>(user))
                    : Result<UserViewModel>.Fail("User not found by username.");
            }

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                user = await _userRepository.GetAsync(u => u.Email == request.Email);
                return user != null
                    ? Result<UserViewModel>.Ok("User found by email.", _mapper.Map<UserViewModel>(user))
                    : Result<UserViewModel>.Fail("User not found by email.");
            }

            return Result<UserViewModel>.Fail("At least one identifier (ID, Email, or Username) is required.");
        }
    }
}
