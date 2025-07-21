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

namespace Application.Features.Auth.Commands.SignIn
{
    public class SignInCommandHandler(
        IAuthService authService,
        IMapper mapper
        ) : IRequestHandler<SignInCommand, Result<UserViewModel>>
    {
        private readonly IAuthService _authService = authService;
        private readonly IMapper _mapper = mapper;
        public async Task<Result<UserViewModel>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var (token, user) = await _authService.Authenticate(request.Login, request.Password);

            return Result<UserViewModel>.LoginSuccess(_mapper.Map<UserViewModel>(user), token);
        }
    }
}
