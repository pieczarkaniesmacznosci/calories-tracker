using API.Dtos;
using API.Mediator.Query;
using AutoMapper;
using Data.Entities;
using Data.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace API.Mediator.Handler
{
    public class GetMealsQueryHandler : IRequestHandler<GetMealsQuery, IEnumerable<MealDto>>
    {
        private readonly IAsyncRepository<Meal> _mealAsyncRepository;
        private readonly IMapper _mapper;
        public GetMealsQueryHandler(
            IAsyncRepository<Meal> mealAsyncRepository,
            IMapper mapper)
        {
            _mealAsyncRepository = mealAsyncRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<MealDto>> Handle(GetMealsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<MealDto>>(await _mealAsyncRepository.FindAsync(x => x.UserId == request.UserId && !x.Deleted));
        }
    }
}
