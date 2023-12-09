using API.Dtos;
using API.Identity;
using API.Result;
using API.Result.ErrorDefinitions;
using API.Validators;
using AutoMapper;
using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public Result<MealLogDto> EditMealLog(int mealLogId, MealDto meal)
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
                    _logger.LogInformation("Meal with id= {mealLogId} was not found!", mealLogId);
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
                _logger.LogError(ex, "Exception while editing meal from {mealDateEaten}", meal.DateEaten);
                return new UnexpectedResult<MealLogDto>();
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
                    _logger.LogInformation("Meal with id= {id} was not found!", id);
                    return new NotFoundResult<MealDto>(string.Format(ErrorDefinitions.NotFoundEntityWithIdError, new string[] { "Meal", id.ToString() }));
                }
                mealToDelete.IsSaved = false;

                var result = _mealRepository.Update(mealToDelete);
                _mealRepository.SaveChanges();
                return new SuccessResult<MealDto>(_mapper.Map<MealDto>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while deleting meal with id= {id}", id);
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
                _logger.LogError(ex, "Exception while getting meals");
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
                _logger.LogError(ex, "Exception while adding meal log");
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
                _logger.LogError(ex, "Exception while deleting meal log");
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
                _logger.LogError(ex, "Exception while getting meal logs");
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
                _logger.LogError(ex, $"Exception while getting meal logs by date");
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
                _logger.LogError(ex, "Exception while getting meal log by id");
                return new UnexpectedResult<MealLogDto>();
            }
        }
    }
}
