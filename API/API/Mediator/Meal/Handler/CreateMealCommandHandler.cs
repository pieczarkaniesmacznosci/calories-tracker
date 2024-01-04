using API.Mediator.Command;
using API.Validators;
using AutoMapper;
using Entities;
using MediatR;
using Repositories;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Mediator.Handler
{
    public class CreateMealCommandHandler : IRequestHandler<CreateMealCommand>
    {
        private readonly IAsyncRepository<Meal> _mealAsyncRepository;
        private readonly IMapper _mapper;
        private readonly IMealValidator _mealValidator;

        public CreateMealCommandHandler(
            IAsyncRepository<Meal> mealAsyncRepository,
            IMapper mapper,
            IMealValidator mealValidator)
        {
            _mealAsyncRepository = mealAsyncRepository;
            _mapper = mapper;
            _mealValidator = mealValidator;
        }

        public async Task Handle(CreateMealCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _mealValidator.Validate(request.Meal);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.Errors.FirstOrDefault().ErrorMessage);
            }

            var mealEntity = _mapper.Map<Meal>(request.Meal);
            mealEntity.UserId = request.UserId;

            await _mealAsyncRepository.AddAsync(mealEntity);
        }
    }
}
