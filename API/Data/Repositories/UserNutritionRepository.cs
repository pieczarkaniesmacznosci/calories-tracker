using Data.DbContexts;
using Data.Entities;

namespace Data.Repositories
{
    public class UserNutritionRepository : GenericRepository<UserNutrition>
    {
        public UserNutritionRepository(CaloriesLibraryContext context) : base(context)
        {
        }
    }
}
