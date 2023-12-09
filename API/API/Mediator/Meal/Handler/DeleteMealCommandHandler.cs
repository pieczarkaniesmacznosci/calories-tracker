using API.Mediator.Command;
using API.Result.ErrorDefinitions;
using Data.Entities;
using Data.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Mediator.Handler
{
    public class DeleteMealCommandHandler : IRequestHandler<DeleteMealCommand>
    {
        private readonly IAsyncRepository<Meal> _mealAsyncRepository;

        public DeleteMealCommandHandler(
            IAsyncRepository<Meal> mealAsyncRepository)
        {
            _mealAsyncRepository = mealAsyncRepository;
        }

        public async Task Handle(DeleteMealCommand request, CancellationToken cancellationToken)
        {
            var mealToDelete = (await _mealAsyncRepository
                            .FindAsync(x => x.UserId == request.UserId && x.Id == request.MealId))
                            .SingleOrDefault();

            if (mealToDelete == null)
            {
                throw new KeyNotFoundException(string.Format(ErrorDefinitions.NotFoundEntityWithIdError, new string[] { "Meal", request.MealId.ToString() }));
            }
            mealToDelete.IsSaved = false;

            var result = _mealAsyncRepository.UpdateAsync(mealToDelete);
        }
    }
}
