using API.Dtos;
using API.Mediator.Query;
using API.Result.ErrorDefinitions;
using AutoMapper;
using Data.Entities;
using Data.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Mediator.Handler
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
            var meal = (await _mealAsyncRepository.FindAsync(x => x.UserId == request.UserId && x.Id == request.MealId)).SingleOrDefault();

            if (meal == null)
            {
                throw new KeyNotFoundException(string.Format(ErrorDefinitions.NotFoundEntityWithIdError, new string[] { "Meal", request.MealId.ToString() }));
            }

            return _mapper.Map<MealDto>(meal);
        }
    }
}
