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

namespace Application.Features.Auth.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler(
        IAuthService authService,
        IMapper mapper
        ) : IRequestHandler<ResetPasswordCommand, Result<UserViewModel>>
    {
        private readonly IAuthService _authService = authService;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<UserViewModel>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _authService.ResetPasswordAsync(request.UserId, request.NewPassword, request.ConformPassword, request.ConfirmationCode);
            return Result<UserViewModel>.Ok(_mapper.Map<UserViewModel>(user));
        }
    }
}
