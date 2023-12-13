using API.Dtos;
using API.Mediator.Query;
using AutoMapper;
using Data.Entities;
using Data.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Mediator.Handler
{
    public class GetMealsByNameQueryHandler : IRequestHandler<GetMealsByNameQuery, IEnumerable<MealDto>>
    {
        private readonly IAsyncRepository<Meal> _asyncMealRepository;
        private readonly IMapper _mapper;

        public GetMealsByNameQueryHandler(
            IAsyncRepository<Meal> asyncMealRepository,
            IMapper mapper)
        {
            _asyncMealRepository = asyncMealRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MealDto>> Handle(GetMealsByNameQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.MealName))
            {
                throw new ArgumentException("MealName cannot be empty or whitespace.");
            }

            //https://entityframeworkcore.com/knowledge-base/43277868/entity-framework-core---contains-is-case-sensitive-or-case-insensitive-
            var meals = (await _asyncMealRepository
                .FindAsync(x => x.UserId == request.UserId && EF.Functions.Like(x.MealName, $"%{request.MealName}%") && !x.Deleted))
                .OrderBy(x => x.DateEaten);

            if (!meals.Any())
            {
                throw new KeyNotFoundException(string.Format(ErrorDefinitions.NotFoundAnyEntityError, new string[] { "Meal" }));
            }

            return _mapper.Map<IEnumerable<MealDto>>(meals);
        }
    }
}
