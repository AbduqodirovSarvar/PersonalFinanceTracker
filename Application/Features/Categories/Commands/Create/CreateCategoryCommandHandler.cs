using Application.Interfaces;
using Application.Models.Common;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Categories.Commands.Create
{
    public class CreateCategoryCommandHandler(
        ICategoryRepositoy repository,
        IMapper mapper,
        ICurrentUserService currentUserService,
        IUserRepositoy userRepositoy)
        : IRequestHandler<CreateCategoryCommand, Result<CategoryViewModel>>
    {
        private readonly ICategoryRepositoy _categoryRepository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IUserRepositoy _userRepository = userRepositoy;

        public async Task<Result<CategoryViewModel>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _userRepository.GetAsync(u => u.Id == _currentUserService.UserId);
            if(currentUser == null)
                return Result<CategoryViewModel>.Fail("You are not authorized to create this category.");

            var category = new Category
            {
                Name = request.Name,
                Color = request.Color,
                UserId = currentUser.Id
            };

            await _categoryRepository.CreateAsync(category);

            return Result<CategoryViewModel>.Ok(_mapper.Map<CategoryViewModel>(category));
        }
    }
}
