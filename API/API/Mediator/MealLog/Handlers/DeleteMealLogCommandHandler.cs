using API.Mediator.Command;
using Entities;
using MediatR;
using Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Mediator.Handler
{
    public class DeleteMealLogCommandHandler : IRequestHandler<DeleteMealLogCommand>
    {
        private readonly IAsyncRepository<MealLog> _mealLogAsyncRepository;

        public DeleteMealLogCommandHandler(IAsyncRepository<MealLog> mealLogAsyncRepository)
        {
            _mealLogAsyncRepository = mealLogAsyncRepository;
        }

        public async Task Handle(DeleteMealLogCommand request, CancellationToken cancellationToken)
        {
            var mealLogToDelete = (await _mealLogAsyncRepository
                .FindAsync(x => x.UserId == request.UserId && x.Id == request.MealLogId))
                .SingleOrDefault();

            if (mealLogToDelete == null)
            {
                throw new KeyNotFoundException(string.Format(ErrorDefinitions.NotFoundEntityWithIdError, new string[] { "Meal", request.MealLogId.ToString() }));
            }

            await _mealLogAsyncRepository.DeleteAsync(mealLogToDelete);
        }
    }
}
