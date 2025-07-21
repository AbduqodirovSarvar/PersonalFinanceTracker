using Application.Interfaces;
using Application.Models;
using Application.Models.Common;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.SignUp
{
    public class SignUpCommandHandler(
        IAuthService authService,
        IMapper mapper
        ) : IRequestHandler<SignUpCommand, Result<UserViewModel>>
    {
        private readonly IAuthService _authService = authService;
        private readonly IMapper _mapper = mapper;
        public async Task<Result<UserViewModel>> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var user = await _authService.RegisterAsync(request.UserName, request.Password, request.Email);

            return Result<UserViewModel>.Ok("User registered successfully.", _mapper.Map<UserViewModel>(user));
        }
    }
}
