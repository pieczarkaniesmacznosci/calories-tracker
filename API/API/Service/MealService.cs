using API.Dtos;
using API.Identity;
using API.Result;
using API.Validators;
using AutoMapper;
using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Result.ErrorDefinitions;

namespace API.Service
{
    public class MealService : IMealService
    {
        private readonly ILogger<MealService> _logger;
        private readonly IRepository<Meal> _mealRepository;
        private readonly IRepository<MealLog> _mealLogRepository;
        private readonly IMapper _mapper;
        private readonly IMealValidator _mealValidator;
        private readonly IUserManager _userManager;
        private int _userId => _userManager.CurrentUserId;


        public MealService(
            ILogger<MealService> logger,
            IRepository<Meal> mealRepository,
            IRepository<MealLog> mealLogRepository,
            IMapper mapper,
            IMealValidator mealValidator,
            IUserManager userManager)
        {
            _logger = logger;
            _mealRepository = mealRepository;
            _mealLogRepository = mealLogRepository;
            _mapper = mapper;
            _mealValidator = mealValidator;
            _userManager = userManager;
        }

        public Result<IEnumerable<MealDto>> GetMeals(bool isSaved)
        {
            try
            {
                var a = _mealRepository.Find(x => x.UserId == _userId && x.IsSaved == isSaved);
                var result = _mapper.Map<IEnumerable<MealDto>>(a);
                return new SuccessResult<IEnumerable<MealDto>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting meals", ex);
                return new UnexpectedResult<IEnumerable<MealDto>>();
            }
        }

        public Result<MealDto> GetMeal(int id)
        {
            try
            {
                var meal = _mealRepository.Find(x => x.UserId == _userId && x.Id == id).FirstOrDefault();

                if (meal == null)
                {
                    _logger.LogInformation($"Meal with id = {id} was not found!");
                    return new NotFoundResult<MealDto>(string.Format(ErrorDefinitions.NotFoundEntityWithIdError, new string[] { "Meal", id.ToString() }));
                }

                var mealDto = _mapper.Map<MealDto>(meal);
                return new SuccessResult<MealDto>(mealDto);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting meal with id = {id}", ex);
                return new UnexpectedResult<MealDto>();
            }
        }

        public Result<bool> MealNameValid(int id, string mealName)
        {
            try
            {
                var meal = _mealRepository
                    .Find(x => x.Id == _userId && x.MealName == mealName && x.Id != id && x.IsSaved)
                    .FirstOrDefault();

                if (meal != null)
                {
                    return new SuccessResult<bool>(false);
                }
                else
                {
                    return new SuccessResult<bool>(true);
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while checking if meal name {mealName} is valid", ex);
                return new UnexpectedResult<bool>();
            }
        }

        public Result<MealDto> AddMeal(MealDto meal)
        {
            try
            {
                var validationResult = _mealValidator.Validate(meal);
                if (!validationResult.IsValid)
                {
                    return new InvalidResult<MealDto>(validationResult.Errors.FirstOrDefault().ErrorMessage);
                }

                var mealEntity = _mapper.Map<Meal>(meal);
                mealEntity.UserId = _userId;

                var result = _mealRepository.Add(mealEntity);

                if (meal.DateEaten != null)
                {
                    _mealLogRepository.Add(new MealLog()
                    {
                        Meal = result,
                        DateEaten = meal.DateEaten.Value,
                        UserId = mealEntity.UserId
                    });
                    _mealLogRepository.SaveChanges();
                }

                _mealRepository.SaveChanges();
                return new SuccessResult<MealDto>(_mapper.Map<MealDto>(result));
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while adding meal from {meal.DateEaten}", ex);
                return new UnexpectedResult<MealDto>();
            }
        }

        public Result<MealLogDto> EditEatenMeal(int mealLogId, MealDto meal)
        {
            try
            {
                var validationResult = _mealValidator.Validate(meal);
                if (!validationResult.IsValid)
                {
                    return new InvalidResult<MealLogDto>(validationResult.Errors.FirstOrDefault().ErrorMessage);
                }

                var mealLogToDelete = _mealLogRepository
                    .Find(x => x.UserId == _userId && x.Id == mealLogId)
                    .FirstOrDefault();

                if (mealLogToDelete == null)
                {
                    _logger.LogInformation($"Meal with id = {mealLogId} was not found!");
                    return new NotFoundResult<MealLogDto>(string.Format(ErrorDefinitions.NotFoundEntityWithIdError, new string[] { "MealLog", mealLogId.ToString() }));
                }

                _mealLogRepository.Delete(mealLogToDelete);
                var mealEntity = _mapper.Map<Meal>(meal);
                mealEntity.UserId = _userId;

                _mealRepository.Add(mealEntity);
                _mealRepository.SaveChanges();

                var mealLogToAdd = new MealLog
                {
                    UserId = _userId,
                    MealId = mealEntity.Id,
                    DateEaten = mealLogToDelete.DateEaten
                };

                _mealLogRepository.Add(mealLogToAdd);
                _mealLogRepository.SaveChanges();

                return new SuccessResult<MealLogDto>(_mapper.Map<MealLogDto>(mealLogToAdd));
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while editing meal from {meal.DateEaten}", ex);
                return new UnexpectedResult<MealLogDto>();
            }
        }
        public Result<MealDto> EditMeal(MealDto meal)
        {
            try
            {
                var validationResult = _mealValidator.Validate(meal);
                if (!validationResult.IsValid)
                {
                    return new InvalidResult<MealDto>(validationResult.Errors.FirstOrDefault().ErrorMessage);
                }

                var productToEdit = _mealRepository.Get(meal.Id.Value);

                if (productToEdit == null)
                {
                    _logger.LogInformation($"Meal with id = {meal.Id} was not found!");
                    return new NotFoundResult<MealDto>(string.Format(ErrorDefinitions.NotFoundEntityWithIdError, new string[] { "Meal", meal.Id.ToString() }));
                }

                var mealEntity = _mapper.Map<Meal>(meal);
                var result = _mealRepository.Update(mealEntity);
                _mealRepository.SaveChanges();

                return new SuccessResult<MealDto>(_mapper.Map<MealDto>(result));
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while editing meal from {meal.DateEaten}", ex);
                return new UnexpectedResult<MealDto>();
            }
        }

        public Result<MealDto> DeleteMeal(int id)
        {
            try
            {
                var mealToDelete = _mealRepository
                    .Find(x => x.UserId == _userId && x.Id == id)
                    .FirstOrDefault();

                if (mealToDelete == null)
                {
                    _logger.LogInformation($"Meal with id = {id} was not found!");
                    return new NotFoundResult<MealDto>(string.Format(ErrorDefinitions.NotFoundEntityWithIdError, new string[] { "Meal", id.ToString() }));
                }
                mealToDelete.IsSaved = false;

                var result = _mealRepository.Update(mealToDelete);
                _mealRepository.SaveChanges();
                return new SuccessResult<MealDto>(_mapper.Map<MealDto>(result));
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while deleting meal with id = {id}", ex);
                return new UnexpectedResult<MealDto>();
            }
        }

        public Result<IEnumerable<MealDto>> GetMeals(string mealName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(mealName))
                {
                    return new InvalidResult<IEnumerable<MealDto>>(null);
                }

                //https://entityframeworkcore.com/knowledge-base/43277868/entity-framework-core---contains-is-case-sensitive-or-case-insensitive-
                var meals = _mealRepository
                    .Find(x => x.IsSaved && x.UserId == _userId && EF.Functions.Like(x.MealName, $"%{mealName}%"))
                    .OrderBy(x => x.DateEaten);

                if (!meals.Any())
                {
                    return new NotFoundResult<IEnumerable<MealDto>>(string.Format(ErrorDefinitions.NotFoundAnyEntityError, new string[] { "Meal" }));
                }

                var mealsDto = _mapper.Map<IEnumerable<MealDto>>(meals);

                return new SuccessResult<IEnumerable<MealDto>>(mealsDto);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting meals", ex);
                return new UnexpectedResult<IEnumerable<MealDto>>();
            }
        }

        public Result<MealLogDto> AddMealLog(MealLogDto mealLog)
        {
            try
            {
                var meal = _mealRepository.Find(x => x.UserId == _userId && x.Id == mealLog.MealId);

                if (meal == null)
                {
                    return new NotFoundResult<MealLogDto>(string.Format(ErrorDefinitions.NotFoundEntityWithIdError, new string[] { "MealLog", mealLog.Id.ToString() }));
                }

                var log = new MealLog()
                {
                    MealId = mealLog.MealId,
                    UserId = _userId,
                    DateEaten = mealLog.DateEaten
                };

                var result = _mealLogRepository.Add(log);
                _mealLogRepository.SaveChanges();

                return new SuccessResult<MealLogDto>(_mapper.Map<MealLogDto>(result));
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while adding meal log", ex);
                return new UnexpectedResult<MealLogDto>();
            }
        }

        public Result<MealLogDto> DeleteMealLog(int mealLogId)
        {
            try
            {
                var mealLog = _mealLogRepository
                    .Find(x => x.UserId == _userId && x.Id == mealLogId)
                    .FirstOrDefault();

                if (mealLog == null)
                {
                    return new NotFoundResult<MealLogDto>(string.Format(ErrorDefinitions.NotFoundEntityWithIdError, new string[] { "MealLog", mealLogId.ToString() }));
                }

                var mealLogDto = _mapper.Map<MealLogDto>(mealLog);

                _mealLogRepository.Delete(mealLog);
                _mealLogRepository.SaveChanges();

                return new SuccessResult<MealLogDto>(mealLogDto);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while deleting meal log", ex);
                return new UnexpectedResult<MealLogDto>();
            }
        }

        public Result<IEnumerable<MealLogDto>> GetMealLog()
        {
            try
            {
                var mealLog = _mealLogRepository
                    .Find(x => x.UserId == _userId)
                    .OrderByDescending(x => x.DateEaten);

                if (!mealLog.Any())
                {
                    return new NotFoundResult<IEnumerable<MealLogDto>>(string.Format(ErrorDefinitions.NotFoundAnyEntityError, new string[] { "MealLog" }));
                }

                var mealLogListDto = _mapper.Map<IEnumerable<MealLog>, IEnumerable<MealLogDto>>(mealLog);

                return new SuccessResult<IEnumerable<MealLogDto>>(mealLogListDto);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting meal logs", ex);
                return new UnexpectedResult<IEnumerable<MealLogDto>>();
            }
        }

        public Result<IEnumerable<MealLogDto>> GetMealLog(DateTime date)
        {
            try
            {
                var mealLog = _mealLogRepository
                    .Find(x => x.UserId == _userId && x.DateEaten.Date.Equals(date.Date));

                if (!mealLog.Any())
                {
                    return new NotFoundResult<IEnumerable<MealLogDto>>(string.Format(ErrorDefinitions.NotFoundAnyEntityError, new string[] { "MealLog" }));
                }

                var mealLogListDto = _mapper.Map<IEnumerable<MealLogDto>>(mealLog);

                return new SuccessResult<IEnumerable<MealLogDto>>(mealLogListDto);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting meal logs by date", ex);
                return new UnexpectedResult<IEnumerable<MealLogDto>>();
            }
        }

        public Result<MealLogDto> GetMealLog(int mealLogId)
        {
            try
            {
                var mealLog = _mealLogRepository
                    .Find(x => x.UserId == _userId && x.Id == mealLogId)
                    .FirstOrDefault();

                if (mealLog == null)
                {
                    return new NotFoundResult<MealLogDto>(string.Format(ErrorDefinitions.NotFoundEntityWithIdError, new string[] { "MealLog", mealLogId.ToString() }));
                }

                var mealLogListDto = _mapper.Map<MealLogDto>(mealLog);

                return new SuccessResult<MealLogDto>(mealLogListDto);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting meal log by id", ex);
                return new UnexpectedResult<MealLogDto>();
            }
        }
    }
}
