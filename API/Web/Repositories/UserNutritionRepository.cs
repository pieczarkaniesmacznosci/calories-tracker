using API.Web.DbContexts;
using API.Web.Entities;

namespace API.Web.Repositories
{
    public class UserNutritionRepository : GenericRepository<UserNutrition>
    {
        public UserNutritionRepository(CaloriesLibraryContext context) : base(context)
        {
        }
    }
}