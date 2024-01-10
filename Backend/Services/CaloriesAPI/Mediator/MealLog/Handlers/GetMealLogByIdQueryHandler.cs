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
    public class GetMealLogByIdQueryHandler : IRequestHandler<GetMealLogByIdQuery, MealLogDto>
    {
        private readonly IAsyncRepository<MealLog> _mealLogAsyncRepository;
        private readonly IMapper _mapper;
        public GetMealLogByIdQueryHandler(
            IAsyncRepository<MealLog> mealLogAsyncRepository,
            IMapper mapper)
        {
            _mealLogAsyncRepository = mealLogAsyncRepository;
            _mapper = mapper;
        }

        public async Task<MealLogDto> Handle(GetMealLogByIdQuery request, CancellationToken cancellationToken)
        {
            MealLog mealLog = (await _mealLogAsyncRepository.FindAsync(x => x.UserId == request.UserId && x.Id == request.MealLogId)).SingleOrDefault();

            if (mealLog == null)
            {
                throw new KeyNotFoundException(string.Format(ErrorDefinitions.NotFoundEntityWithIdError, new string[] { "MealLog", request.MealLogId.ToString() }));
            }

            return _mapper.Map<MealLogDto>(mealLog);
        }
    }
}
