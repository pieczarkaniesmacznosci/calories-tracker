using AutoMapper;
using CaloriesAPI.Mediator.Command;
using CaloriesAPI.Validators;
using Entities;
using MediatR;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CaloriesAPI.Mediator.Handler
{
    public class EditMealCommandHandler : IRequestHandler<EditMealCommand>
    {
        private readonly IAsyncRepository<Meal> _mealAsyncRepository;
        private readonly IMapper _mapper;
        private readonly IMealValidator _mealValidator;

        public EditMealCommandHandler(
            IAsyncRepository<Meal> mealAsyncRepository,
            IMapper mapper,
            IMealValidator mealValidator)
        {
            _mealAsyncRepository = mealAsyncRepository;
            _mapper = mapper;
            _mealValidator = mealValidator;
        }

        public async Task Handle(EditMealCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _mealValidator.Validate(request.Meal);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.Errors.FirstOrDefault().ErrorMessage);
            }

            var productToEdit = _mealAsyncRepository.GetByIdAsync(request.MealId);

            if (productToEdit == null)
            {
                throw new KeyNotFoundException(string.Format(ErrorDefinitions.NotFoundEntityWithIdError, new string[] { "Meal", request.MealId.ToString() }));
            }

            var mealEntity = _mapper.Map<Meal>(request.Meal);
            await _mealAsyncRepository.UpdateAsync(mealEntity);
            await _mealAsyncRepository.AddAsync(mealEntity);
        }
    }
}
