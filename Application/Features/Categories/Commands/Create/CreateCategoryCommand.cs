using Application.Models.Common;
using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Categories.Commands.Create
{
    public record CreateCategoryCommand(
        string Name, 
        string Color) : IRequest<Result<CategoryViewModel>>;
}
