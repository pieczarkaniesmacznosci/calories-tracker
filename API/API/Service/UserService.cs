using API.Dtos;
using API.Extensions;
using API.Result;
using AutoMapper;
using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Result.ErrorDefinitions;

namespace API.Service
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IRepository<UserWeight> _userWeightRepository;
        private readonly IRepository<UserNutrition> _userNutritionRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        private int _userId => GetCurrentUserId().Result;

        private async Task<int> GetCurrentUserId()
        {
            var loggedInUserName = _httpContextAccessor.HttpContext.User.GetLoggedInUserName();
            var currentUserId = await _userManager.FindByNameAsync(loggedInUserName);
            return currentUserId.Id;
        }
        public UserService(
            ILogger<UserService> logger,
            IRepository<UserWeight> userWeightRepository,
            IRepository<UserNutrition> userNutritionRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager)
        {
            _logger = logger;
            _userWeightRepository = userWeightRepository;
            _userNutritionRepository = userNutritionRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public Result<UserNutritionDto> AddUserNutrition(UserNutritionDto userNutrition)
        {
            try
            {
                var userNutritionEntity = _mapper.Map<UserNutrition>(userNutrition);
                userNutritionEntity.UserId = _userId;
                var result = _userNutritionRepository.Add(userNutritionEntity);
                _userNutritionRepository.SaveChanges();
                return new SuccessResult<UserNutritionDto>(_mapper.Map<UserNutritionDto>(result));
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while adding user untrition from {userNutrition.Date}", ex);
                return new UnexpectedResult<UserNutritionDto>();
            }
        }


        public Result<UserWeightDto> AddUserWeight(UserWeightDto userWeight)
        {
            try
            {
                userWeight.UserId = _userId;
                var userWeightEntity = _mapper.Map<UserWeight>(userWeight);
                var result = _userWeightRepository.Add(userWeightEntity);
                _userWeightRepository.SaveChanges();
                return new SuccessResult<UserWeightDto>(_mapper.Map<UserWeightDto>(result));
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while adding user untrition from {userWeight.Date}", ex);
                return new UnexpectedResult<UserWeightDto>();
            }
        }

        public Result<UserNutritionDto> DeleteUserNutrition(UserNutritionDto userNutrition)
        {
            try
            {
                var userNutritionToDelete = _userNutritionRepository.Find(x => x.UserId == userNutrition.Id && x.UserId == _userId).FirstOrDefault();

                if (userNutritionToDelete == null)
                {
                    _logger.LogInformation($"User nutrition with id = {userNutrition.Id} was not found!");
                    return new NotFoundResult<UserNutritionDto>(string.Format(ErrorDefinitions.NotFoundEntityWithIdError, new string[] { "UserNutrition", userNutrition.Id.ToString() }));
                }

                var result = _userNutritionRepository.Delete(userNutritionToDelete);
                _userNutritionRepository.SaveChanges();

                return new SuccessResult<UserNutritionDto>(_mapper.Map<UserNutritionDto>(result));
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while deleting user nutrition", ex);
                return new UnexpectedResult<UserNutritionDto>();
            }
        }

        public Result<UserWeightDto> DeleteUserWeight(UserWeightDto userWeight)
        {
            try
            {
                var userNutritionToDelete = _userNutritionRepository.Find(x => x.UserId == userWeight.Id && x.UserId == _userId).FirstOrDefault();

                if (userNutritionToDelete == null)
                {
                    _logger.LogInformation($"User weight with id = {userWeight.Id} was not found!");
                    return new NotFoundResult<UserWeightDto>(string.Format(ErrorDefinitions.NotFoundEntityWithIdError, new string[] { "UserNutrition", userWeight.Id.ToString() }));
                }

                var result = _userNutritionRepository.Delete(userNutritionToDelete);
                _userNutritionRepository.SaveChanges();

                return new SuccessResult<UserWeightDto>(_mapper.Map<UserWeightDto>(result));
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while deleting user weight", ex);
                return new UnexpectedResult<UserWeightDto>();
            }
        }

        public Result<UserNutritionDto> EditUserNutrition(UserNutritionDto userNutrition)
        {
            try
            {
                var userNutritionToEdit = _userNutritionRepository.Find(x => x.UserId == userNutrition.Id && x.UserId == _userId).FirstOrDefault();

                if (userNutritionToEdit == null)
                {
                    _logger.LogInformation($"User nutrition with id = {userNutrition.Id} was not found!");
                    return new NotFoundResult<UserNutritionDto>(string.Format(ErrorDefinitions.NotFoundEntityWithIdError, new string[] { "UserNutrition", userNutrition.Id.ToString() }));
                }

                var userNutritionEntity = _mapper.Map<UserNutrition>(userNutritionToEdit);
                var result = _userNutritionRepository.Update(userNutritionEntity);
                _userNutritionRepository.SaveChanges();

                return new SuccessResult<UserNutritionDto>(_mapper.Map<UserNutritionDto>(result));
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while editing user nutrition with id = {userNutrition.Id}", ex);
                return new UnexpectedResult<UserNutritionDto>();
            }
        }

        public Result<UserWeightDto> EditUserWeight(UserWeightDto userWeight)
        {
            try
            {
                var userNutritionToEdit = _userWeightRepository.Find(x => x.UserId == userWeight.Id && x.UserId == _userId).FirstOrDefault();

                if (userNutritionToEdit == null)
                {
                    _logger.LogInformation($"User weight with id = {userWeight.Id} was not found!");
                    return new NotFoundResult<UserWeightDto>(string.Format(ErrorDefinitions.NotFoundEntityWithIdError, new string[] { "UserWeight", userWeight.Id.ToString() }));
                }

                var userWeightEntity = _mapper.Map<UserWeight>(userNutritionToEdit);
                var result = _userWeightRepository.Update(userWeightEntity);
                _userWeightRepository.SaveChanges();

                return new SuccessResult<UserWeightDto>(_mapper.Map<UserWeightDto>(result));
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while editing user weight with id = {userWeight.Id}", ex);
                return new UnexpectedResult<UserWeightDto>();
            }
        }

        public Result<UserNutritionDto> GetCurrentUserNutrition()
        {
            try
            {
                var currentUserNutrition = _userNutritionRepository
                    .Find(x => x.UserId == _userId)
                    .OrderByDescending(x => x.Date)
                    .FirstOrDefault();

                if (currentUserNutrition == null)
                {
                    _logger.LogInformation($"Current user nutrition was not found!");
                    return new NotFoundResult<UserNutritionDto>();
                }

                var result = _mapper.Map<UserNutritionDto>(currentUserNutrition);
                return new SuccessResult<UserNutritionDto>(result);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting meals", ex);
                return new UnexpectedResult<UserNutritionDto>();
            }
        }

        public Result<UserWeightDto> GetCurrentUserWeight()
        {
            try
            {
                var currentUserWeight = _userWeightRepository
                .Find(x => x.UserId == _userId)
                .OrderByDescending(x => x.Date)
                .FirstOrDefault();

                if (currentUserWeight == null)
                {
                    _logger.LogInformation($"Current user weight was not found!");
                    return new NotFoundResult<UserWeightDto>();
                }

                var result = _mapper.Map<UserWeightDto>(currentUserWeight);
                return new SuccessResult<UserWeightDto>(result);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting meals", ex);
                return new UnexpectedResult<UserWeightDto>();
            }
        }

        public Result<UserNutritionDto> GetUserNutrition(DateTime date)
        {
            try
            {
                var userNutrition = _userNutritionRepository.Find(x => x.Date.Date == date.Date && x.UserId == _userId);

                if (userNutrition == null)
                {
                    _logger.LogInformation($"User nutrition with date = {date} was not found!");
                    return new NotFoundResult<UserNutritionDto>();
                }

                var userNutritionDto = _mapper.Map<UserNutritionDto>(userNutrition);
                return new SuccessResult<UserNutritionDto>(userNutritionDto);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting user nutrition with date = {date}", ex);
                return new UnexpectedResult<UserNutritionDto>();
            }
        }

        public Result<IEnumerable<UserNutritionDto>> GetUserNutritions()
        {
            try
            {
                var result = _mapper.Map<IEnumerable<UserNutritionDto>>(_userNutritionRepository.Find(x => x.UserId == _userId));
                return new SuccessResult<IEnumerable<UserNutritionDto>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting user nutritions", ex);
                return new UnexpectedResult<IEnumerable<UserNutritionDto>>();
            }
        }

        public Result<UserWeightDto> GetUserWeight(DateTime date)
        {
            try
            {
                var userWeight = _userWeightRepository.Find(x => x.Date.Date == date.Date && x.UserId == _userId);

                if (userWeight == null)
                {
                    _logger.LogInformation($"User weight with date = {date} was not found!");
                    return new NotFoundResult<UserWeightDto>();
                }

                var userWeightDto = _mapper.Map<UserWeightDto>(userWeight);
                return new SuccessResult<UserWeightDto>(userWeightDto);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting user weight with date = {date}", ex);
                return new UnexpectedResult<UserWeightDto>();
            }
        }

        public Result<IEnumerable<UserWeightDto>> GetUserWeights()
        {
            try
            {
                var result = _mapper.Map<IEnumerable<UserWeightDto>>(_userWeightRepository.Find(x => x.UserId == _userId));
                return new SuccessResult<IEnumerable<UserWeightDto>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting user weights", ex);
                return new UnexpectedResult<IEnumerable<UserWeightDto>>();
            }
        }
    }
}
