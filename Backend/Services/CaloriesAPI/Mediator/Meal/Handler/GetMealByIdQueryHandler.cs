using AutoMapper;
using CaloriesAPI.Dtos;
using CaloriesAPI.Mediator.Query;
using Entities;
using MediatR;
using Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CaloriesAPI.Mediator.Handler
{
    public class GetMealByIdQueryHandler : IRequestHandler<GetMealByIdQuery, MealDto>
    {
        private readonly IAsyncRepository<Meal> _mealAsyncRepository;
        private readonly IMapper _mapper;
        public GetMealByIdQueryHandler(
            IAsyncRepository<Meal> mealAsyncRepository,
            IMapper mapper)
        {
            _mealAsyncRepository = mealAsyncRepository;
            _mapper = mapper;
        }

        public async Task<MealDto> Handle(GetMealByIdQuery request, CancellationToken cancellationToken)
        {
            var meal = (await _mealAsyncRepository.FindAsync(x => x.UserId == request.UserId && x.Id == request.MealId && !x.Deleted)).SingleOrDefault();

            if (meal == null)
            {
                throw new KeyNotFoundException(string.Format(ErrorDefinitions.NotFoundEntityWithIdError, new string[] { "Meal", request.MealId.ToString() }));
            }

            return _mapper.Map<MealDto>(meal);
        }
    }
}
