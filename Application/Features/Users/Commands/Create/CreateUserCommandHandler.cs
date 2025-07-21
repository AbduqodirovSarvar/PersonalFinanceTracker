using Application.Interfaces;
using Application.Models;
using Application.Models.Common;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.Create
{
    public class CreateUserCommandHandler(
        IUserRepositoy userRepository,
        IHashService hashService,
        IMapper mapper) 
        : IRequestHandler<CreateUserCommand, Result<UserViewModel>>
    {
        private readonly IUserRepositoy _userRepository = userRepository;
        private readonly IHashService _hashService = hashService;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<UserViewModel>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetAsync(u => u.Email == request.Email);
            if (existingUser != null)
                return Result<UserViewModel>.Fail("This email is already registered.");

            var passwordHash = _hashService.Hash(request.Password);

            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                PasswordHash = passwordHash,
                Role = request.Role
            };

            var savedUser = await _userRepository.CreateAsync(user);

            var viewModel = _mapper.Map<UserViewModel>(savedUser);

            return Result<UserViewModel>.Ok("User successfully created.", viewModel);
        }
    }
}
