using System;
using System.Collections.Generic;
using API.Web.Entities;
using API.Web.Dtos;
using API.Web.Repositories;
using API.Web.Result;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Linq;
using API.Web.Validators;
using Microsoft.EntityFrameworkCore;

namespace API.Web.Service
{
    public class MealService : IMealService
    {
        private readonly ILogger<MealService> _logger;
        private readonly IRepository<Meal> _mealRepository;
        private readonly IRepository<MealLog> _mealLogRepository;
        private readonly IMapper _mapper;
        private readonly MealValidator _mealValidator;


        public MealService(ILogger<MealService> logger, IRepository<Meal> mealRepository,IRepository<MealLog> mealLogRepository, IMapper mapper, MealValidator mealValidator)
        {
            _logger = logger;
            _mealRepository = mealRepository;
            _mealLogRepository = mealLogRepository;
            _mapper = mapper;
            _mealValidator = mealValidator;
        }

        public Result<IEnumerable<MealDto>> GetMeals(bool isSaved)
        {
            try
            {
                var a = _mealRepository.Find(x=>x.IsSaved == isSaved);
                var result = _mapper.Map<IEnumerable<MealDto>>(a);
                return new SuccessResult<IEnumerable<MealDto>>(result);
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
                var validationResult = _mealValidator.Validate(meal);
                if(!validationResult.IsValid)
                {
                    return new InvalidResult<MealDto>(validationResult.Errors.FirstOrDefault().ErrorMessage);
                }
                
                var productEntity = _mapper.Map<Meal>(meal);
                productEntity.UserId =1;
                var result = _mealRepository.Add(productEntity);
                if(meal.DateEaten != null)
                {
                    _mealLogRepository.Add(new MealLog(){
                        Meal = result,
                        DateEaten = meal.DateEaten.Value,
                        UserId =productEntity.UserId
                    });
                }

                _mealRepository.SaveChanges();
                return new SuccessResult<MealDto>(_mapper.Map<MealDto>(result));
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while adding meal from {meal.DateEaten}",ex);
                return new UnexpectedResult<MealDto>();
            }
        }

        public Result<MealDto> EditMeal(MealDto meal)
        {
            try
            {            
                var validationResult = _mealValidator.Validate(meal);
                if(!validationResult.IsValid)
                {
                    return new InvalidResult<MealDto>(validationResult.Errors.FirstOrDefault().ErrorMessage);
                }
                    
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
                _logger.LogCritical($"Exception while editing meal from {meal.DateEaten}",ex);
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

        public Result<IEnumerable<MealDto>> GetMeals(string mealName)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(mealName))
                {
                    return new NotFoundResult<IEnumerable<MealDto>>();
                }

                //https://entityframeworkcore.com/knowledge-base/43277868/entity-framework-core---contains-is-case-sensitive-or-case-insensitive-
                var meals = _mealRepository.Find(x=> x.IsSaved && x.UserId ==1 && EF.Functions.Like(x.MealName, $"%{mealName}%"));

                if(!meals.Any())
                {
                    return new NotFoundResult<IEnumerable<MealDto>>();
                }
                
                var mealsDto = _mapper.Map<IEnumerable<MealDto>>(meals);

                return new SuccessResult<IEnumerable<MealDto>>(mealsDto);                
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while getting meals",ex);
                return new UnexpectedResult<IEnumerable<MealDto>>();
            }
        }

        public Result<MealLogDto> AddMealLog(MealLogDto mealLog)
        {
            try
            {
                var meal = _mealRepository.Get(mealLog.MealId);

                if(meal ==null)
                {
                    return new NotFoundResult<MealLogDto>();
                }

                var log = new MealLog(){
                    MealId = mealLog.MealId,
                    UserId = 1,
                    DateEaten = mealLog.DateEaten ?? DateTime.Now
                };

                var result = _mealLogRepository.Add(log);
                _mealLogRepository.SaveChanges();
                
                return new SuccessResult<MealLogDto>(_mapper.Map<MealLogDto>(result));                
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while adding meal log",ex);
                return new UnexpectedResult<MealLogDto>();
            }
        }

        public Result<MealLogDto> DeleteMealLog(int mealLogId)
        {
            try
            {
                var mealLog = _mealLogRepository.Get(mealLogId);

                if(mealLog ==null)
                {
                    return new NotFoundResult<MealLogDto>();
                }

                var mealLogDto = _mapper.Map<MealLogDto>(mealLog);

                _mealLogRepository.Delete(mealLog);
                _mealLogRepository.SaveChanges();

                return new SuccessResult<MealLogDto>(mealLogDto);                
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while deleting meal log",ex);
                return new UnexpectedResult<MealLogDto>();
            }
        }

        public Result<IEnumerable<MealLogDto>> GetMealLog()
        {
            try
            {
                var mealLog = _mealLogRepository.All().OrderByDescending(x=>x.DateEaten);

                if(!mealLog.Any())
                {
                    return new NotFoundResult<IEnumerable<MealLogDto>>();
                }

                var mealLogListDto = _mapper.Map<IEnumerable<MealLog>,IEnumerable<MealLogDto>>(mealLog);

                return new SuccessResult<IEnumerable<MealLogDto>>(mealLogListDto);                
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while getting meal logs",ex);
                return new UnexpectedResult<IEnumerable<MealLogDto>>();
            }
        }

        public Result<IEnumerable<MealLogDto>> GetMealLog(DateTime date)
        {
            try
            {
                var mealLog = _mealLogRepository.Find(x=>x.DateEaten.Date == date.Date).OrderByDescending(x=>x.DateEaten);

                if(!mealLog.Any())
                {
                    return new NotFoundResult<IEnumerable<MealLogDto>>();
                }

                var mealLogListDto = _mapper.Map<IEnumerable<MealLogDto>>(mealLog);

                return new SuccessResult<IEnumerable<MealLogDto>>(mealLogListDto);                
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception while getting meal logs by date",ex);
                return new UnexpectedResult<IEnumerable<MealLogDto>>();
            }
        }
    }
}