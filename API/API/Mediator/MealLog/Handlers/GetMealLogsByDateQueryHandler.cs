using API.Dtos;
using API.Mediator.Query;
using AutoMapper;
using Entities;
using MediatR;
using Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Mediator.Handler
{
    public class GetMealLogsByDateQueryHandler : IRequestHandler<GetMealLogsByDateQuery, IEnumerable<MealLogDto>>
    {
        private readonly IAsyncRepository<MealLog> _asyncMealLogRepository;
        private readonly IMapper _mapper;

        public GetMealLogsByDateQueryHandler(
            IAsyncRepository<MealLog> asyncMealLogRepository,
            IMapper mapper)
        {
            _asyncMealLogRepository = asyncMealLogRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MealLogDto>> Handle(GetMealLogsByDateQuery request, CancellationToken cancellationToken)
        {
            var mealLog = await _asyncMealLogRepository
                    .FindAsync(x => x.UserId == request.UserId && x.DateEaten.Date.Equals(request.MealLogDate));

            if (!mealLog.Any())
            {
                throw new KeyNotFoundException(string.Format(ErrorDefinitions.NotFoundAnyEntityError, new string[] { "MealLog" }));
            }

            return _mapper.Map<IEnumerable<MealLogDto>>(mealLog);
        }
    }
}
