using System;
using System.Collections.Generic;
using API.Web.Entities;
using API.Web.Dtos;
using API.Web.Repositories;
using API.Web.Result;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace API.Web.Service
{
    public class MealService : IMealService
    {
        private readonly ILogger<MealService> _logger;
        private readonly IRepository<Meal> _mealRepository;
        private readonly IMapper _mapper;

        public MealService(ILogger<MealService> logger, IRepository<Meal> productRepository, IMapper mapper)
        {
            _logger = logger;
            _mealRepository = productRepository;
            _mapper = mapper;
        }

        public Result<IEnumerable<MealDto>> GetMeals()
        {
            try
            {
                var result = _mapper.Map<IEnumerable<MealDto>>(_mealRepository.All());
                return new SuccessResult<IEnumerable<MealDto>>(result);
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while getting meals",ex);
                return new UnexpectedResult<IEnumerable<MealDto>>();
            }
        }

        public Result<IEnumerable<MealDto>> GetMeals(DateTime mealDate)
        {
            try
            {
                var meals = _mealRepository
                    .All()
                    .Where(x=>x.Date.Date == mealDate.Date)
                    .ToList();

                if(meals.Count == 0)
                {
                    _logger.LogInformation($"No meals from {mealDate} were found!");
                    return new NotFoundResult<IEnumerable<MealDto>>();
                }

                var mealDtos = _mapper.Map<IEnumerable<MealDto>>(meals);
                return new SuccessResult<IEnumerable<MealDto>>(mealDtos);
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while getting meals",ex);
                return new UnexpectedResult<IEnumerable<MealDto>>();
            }
        }

        public Result<MealDto> GetMeal(int id)
        {
            try
            {                
                var meal = _mealRepository.Get(id);

                if(meal == null)
                {
                    _logger.LogInformation($"Meal with id = {id} was not found!");
                    return new NotFoundResult<MealDto>();
                }

                var mealDto = _mapper.Map<MealDto>(meal);
                return new SuccessResult<MealDto>(mealDto);
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while getting meal with id = {id}",ex);
                return new UnexpectedResult<MealDto>();
            }
        }

        public Result<MealDto> AddMeal(MealDto meal)
        {
            try
            {
                var productEntity = _mapper.Map<Meal>(meal);
                var result = _mealRepository.Add(productEntity);
                _mealRepository.SaveChanges();
                return new SuccessResult<MealDto>(_mapper.Map<MealDto>(result));
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while adding meal from {meal.Date}",ex);
                return new UnexpectedResult<MealDto>();
            }
        }

        public Result<MealDto> EditMeal(MealDto meal)
        {
            try
            {                
                var productToEdit = _mealRepository.Get(meal.Id);

                if(productToEdit == null)
                {
                    _logger.LogInformation($"Meal with id = {meal.Id} was not found!");
                    return new NotFoundResult<MealDto>();
                }

                var mealEntity = _mapper.Map<Meal>(meal);
                var result = _mealRepository.Update(mealEntity);
                _mealRepository.SaveChanges();

                return new SuccessResult<MealDto>(_mapper.Map<MealDto>(result));
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while editing meal from {meal.Date}",ex);
                return new UnexpectedResult<MealDto>();
            }
        }

        public Result<MealDto> DeleteMeal(int id)
        {
            try
            {
                var mealToDelete = _mealRepository.Get(id);

                if(mealToDelete == null)
                {
                    _logger.LogInformation($"Meal with id = {id} was not found!");
                    return new NotFoundResult<MealDto>();
                }

                var result = _mealRepository.Delete(mealToDelete);
                _mealRepository.SaveChanges();
                return new SuccessResult<MealDto>(_mapper.Map<MealDto>(result));
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while deleting meal with id = {id}",ex);
                return new UnexpectedResult<MealDto>();
            }
        }
    }
}