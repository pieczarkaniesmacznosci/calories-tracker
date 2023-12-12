using API.Dtos;
using API.Mediator.Query;
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
    public class GetMealLogsQueryHandler : IRequestHandler<GetMealLogsQuery, IEnumerable<MealLogDto>>
    {
        private readonly IAsyncRepository<MealLog> _asyncMealLogRepository;
        private readonly IMapper _mapper;
        public GetMealLogsQueryHandler(
            IAsyncRepository<MealLog> asyncMealLogRepository,
            IMapper mapper)
        {
            _asyncMealLogRepository = asyncMealLogRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MealLogDto>> Handle(GetMealLogsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<MealLog> mealLogs = (await _asyncMealLogRepository.FindAsync(x => x.UserId == request.UserId)).OrderByDescending(x => x.DateEaten);

            return _mapper.Map<IEnumerable<MealLogDto>>(mealLogs);
        }
    }
}
