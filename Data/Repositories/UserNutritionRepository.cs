using Data.Entities;
using Data.Web.DbContexts;

namespace Data.Repositories
{
    public class UserNutritionRepository : GenericRepository<UserNutrition>
    {
        public UserNutritionRepository(CaloriesLibraryContext context) : base(context)
        {
        }
    }
}
