namespace UserService.Service
{
    //public class UserService : IUserService
    //{
    //    private readonly IMapper _mapper;

    //    public UserService(
    //        IAsyncRepository<UserWeight> userWeightAsyncRepository,
    //        IAsyncRepository<UserNutrition> userNutritionAsyncRepository,
    //        IMapper mapper)
    //    {
    //        _userWeightAsyncRepository = userWeightAsyncRepository;
    //        _userNutritionAsyncRepository = userNutritionAsyncRepository;
    //        _mapper = mapper;
    //    }

    //    public async Task<IEnumerable<UserWeightDto>> GetUserWeightsAsync(int userId)
    //    {
    //        return _mapper.Map<IEnumerable<UserWeightDto>>(await _userWeightAsyncRepository.FindAsync(x => x.UserId == userId));
    //    }

    //    public async Task<UserWeightDto> GetUserWeightAsync(int userId)
    //    {
    //        var currentUserWeight = (await _userWeightAsyncRepository
    //        .FindAsync(x => x.UserId == userId))
    //        .OrderByDescending(x => x.Date)
    //        .FirstOrDefault();

    //        if (currentUserWeight == null)
    //        {
    //            throw new KeyNotFoundException("Current user weight was not found!");
    //        }

    //        return _mapper.Map<UserWeightDto>(currentUserWeight);
    //    }

    //    public async Task<UserWeightDto> GetUserWeightAsync(int userId, DateTime date)
    //    {
    //        var userWeight = await _userWeightAsyncRepository.FindAsync(x => x.Date.Date == date.Date && x.UserId == userId);

    //        if (userWeight == null)
    //        {
    //            throw new KeyNotFoundException($"User weight for date= {date} was not found!");
    //        }

    //        return _mapper.Map<UserWeightDto>(userWeight);
    //    }

    //    public async Task AddUserWeightAsync(UserWeightDto userWeight)
    //    {
    //        var userWeightEntity = _mapper.Map<UserWeight>(userWeight);
    //        await _userWeightAsyncRepository.AddAsync(userWeightEntity);
    //    }

    //    public async Task EditUserWeightAsync(UserWeightDto userWeight)
    //    {
    //        var userNutritionToEdit = (await _userWeightAsyncRepository.FindAsync(x => x.UserId == userWeight.Id && x.UserId == userWeight.UserId)).SingleOrDefault();

    //        if (userNutritionToEdit == null)
    //        {
    //            throw new KeyNotFoundException(string.Format(ErrorDefinitions.NotFoundEntityWithIdError, new string[] { "UserWeight", userWeight.Id.ToString() }));
    //        }

    //        var userWeightEntity = _mapper.Map<UserWeight>(userNutritionToEdit);
    //        var result = _userWeightAsyncRepository.UpdateAsync(userWeightEntity);
    //    }

    //    public async Task DeleteUserWeightAsync(int userId, int userWeightId)
    //    {
    //        var userWeightToDelete = (await _userNutritionAsyncRepository.FindAsync(x => x.Id == userWeightId && x.UserId == userId)).FirstOrDefault();

    //        if (userWeightToDelete == null)
    //        {
    //            throw new KeyNotFoundException(string.Format(ErrorDefinitions.NotFoundEntityWithIdError, new string[] { "UserWeight", userWeightId.ToString() }));
    //        }

    //        await _userNutritionAsyncRepository.DeleteAsync(userWeightToDelete);
    //    }

    //    public async Task<UserNutritionDto> GetUserCurrentNutritionAsync(int userId)
    //    {
    //        var currentUserNutrition = (await _userNutritionAsyncRepository
    //            .FindAsync(x => x.UserId == userId))
    //            .OrderByDescending(x => x.Date)
    //            .FirstOrDefault();

    //        if (currentUserNutrition == null)
    //        {
    //            throw new KeyNotFoundException("Current user nutrition was not found!");
    //        }

    //        return _mapper.Map<UserNutritionDto>(currentUserNutrition);
    //    }

    //    public async Task<UserNutritionDto> GetUserNutritionAsync(int userId, DateTime date)
    //    {
    //        var userNutrition = await _userNutritionAsyncRepository.FindAsync(x => x.Date.Date == date.Date && x.UserId == userId);

    //        if (userNutrition == null)
    //        {
    //            throw new KeyNotFoundException($"User nutrition for date= {date} was not found!");
    //        }

    //        return _mapper.Map<UserNutritionDto>(userNutrition);
    //    }

    //    public async Task<IEnumerable<UserNutritionDto>> GetUserNutritionsAsync(int userId)
    //    {
    //        return _mapper.Map<IEnumerable<UserNutritionDto>>(await _userNutritionAsyncRepository.FindAsync(x => x.UserId == userId));
    //    }

    //    public async Task AddUserNutritionAsync(int userId, UserNutritionDto userNutrition)
    //    {
    //        var userNutritionEntity = _mapper.Map<UserNutrition>(userNutrition);
    //        userNutritionEntity.UserId = userId;
    //        await _userNutritionAsyncRepository.AddAsync(userNutritionEntity);
    //    }

    //    public async Task EditUserNutritionAsync(int userId, UserNutritionDto userNutrition)
    //    {
    //        var userNutritionToEdit = (await _userNutritionAsyncRepository.FindAsync(x => x.UserId == userNutrition.Id && x.UserId == userId)).FirstOrDefault();

    //        if (userNutritionToEdit == null)
    //        {
    //            throw new KeyNotFoundException(string.Format(ErrorDefinitions.NotFoundEntityWithIdError, new string[] { "UserNutrition", userNutrition.Id.ToString() }));
    //        }

    //        var userNutritionEntity = _mapper.Map<UserNutrition>(userNutritionToEdit);
    //        var result = _userNutritionAsyncRepository.UpdateAsync(userNutritionEntity);
    //    }

    //    public async Task DeleteUserNutritionAsync(int userId, int userNutritionId)
    //    {
    //        var userNutritionToDelete = (await _userNutritionAsyncRepository.FindAsync(x => x.UserId == userNutritionId && x.UserId == userId)).SingleOrDefault();

    //        if (userNutritionToDelete == null)
    //        {
    //            throw new KeyNotFoundException(string.Format(ErrorDefinitions.NotFoundEntityWithIdError, new string[] { "UserNutrition", userNutritionId.ToString() }));
    //        }

    //        await _userNutritionAsyncRepository.DeleteAsync(userNutritionToDelete);
    //    }
    //}
}
