using Application.Models;
using Application.Models.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.SignIn
{
    public class SignInCommandHandler(
        
        ) : IRequestHandler<SignInCommand, Result<UserViewModel>>
    {
        public Task<Result<UserViewModel>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
