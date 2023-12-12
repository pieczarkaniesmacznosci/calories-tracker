using API.Mediator.Command;
using API.Validators;
using AutoMapper;
using Data.Entities;
using Data.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Mediator.Handler
{
    public class EditMealLogCommandHandler : IRequestHandler<EditMealLogCommand>
    {
        private readonly IAsyncRepository<MealLog> _mealLogAsyncRepository;
        private readonly IMapper _mapper;
        private readonly IMealValidator _mealValidator;

        public EditMealLogCommandHandler(
            IAsyncRepository<MealLog> mealLogAsyncRepository,
            IMapper mapper,
            IMealValidator mealValidator)
        {
            _mealLogAsyncRepository = mealLogAsyncRepository;
            _mapper = mapper;
            _mealValidator = mealValidator;
        }

        public async Task Handle(EditMealLogCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _mealValidator.Validate(request.MealLog.Meal);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.Errors.FirstOrDefault().ErrorMessage);
            }

            var mealLogToUpdate = (await _mealLogAsyncRepository
                .FindAsync(x => x.UserId == request.UserId && x.Id == request.MealLogId)
                ).SingleOrDefault();

            if (mealLogToUpdate == null)
            {
                throw new KeyNotFoundException(string.Format(ErrorDefinitions.NotFoundEntityWithIdError, new string[] { "Meal", request.MealLogId.ToString() }));
            }

            mealLogToUpdate.Meal = _mapper.Map<Meal>(request.MealLog.Meal);
            mealLogToUpdate.DateEaten = request.MealLog.DateEaten;

            await _mealLogAsyncRepository.UpdateAsync(mealLogToUpdate);
        }
    }
}
