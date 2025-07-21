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

namespace Application.Features.Auth.Commands.ChangeRole
{
    public class ChangeRoleCommandHandler(
        IAuthService authService,
        IMapper mapper
        ) : IRequestHandler<ChangeRoleCommand, Result<UserViewModel>>
    {
        private readonly IAuthService _authService = authService;
        private readonly IMapper _mapper = mapper;
        public async Task<Result<UserViewModel>> Handle(ChangeRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _authService.ChangeRoleAsync(request.UserId, request.NewRole);
            return Result<UserViewModel>.Ok(_mapper.Map<UserViewModel>(user));
        }
    }
}
