using API.Mediator.Command;
using API.Mediator.Query;
using Data.Entities;
using Data.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace API.Mediator.Handler
{
    public class CreateMealLogCommandHandler : IRequestHandler<CreateMealLogCommand>
    {
        private readonly IMediator _mediator;
        private readonly IAsyncRepository<MealLog> _mealLogAsyncRepository;

        public CreateMealLogCommandHandler(
            IMediator mediator,
            IAsyncRepository<MealLog> mealLogAsyncRepository)
        {
            _mediator = mediator;
            _mealLogAsyncRepository = mealLogAsyncRepository;
        }

        public async Task Handle(CreateMealLogCommand request, CancellationToken cancellationToken)
        {
            var meal = _mediator.Send(new GetMealByIdQuery()
            {
                UserId = request.UserId,
                MealId = request.MealLog.MealId
            }, cancellationToken);

            MealLog mealLog = new()
            {
                MealId = meal.Id,
                UserId = request.UserId,
                DateEaten = request.MealLog.DateEaten
            };

            await _mealLogAsyncRepository.AddAsync(mealLog);
        }
    }
}
