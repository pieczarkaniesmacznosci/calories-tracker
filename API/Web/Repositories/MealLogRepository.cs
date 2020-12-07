using API.Web.DbContexts;
using API.Web.Entities;
using API.Web.Repositories;

namespace API.Web.Repositories
{
    public class MealLogRepository : GenericRepository<MealLog>
    {
        
        public MealLogRepository(CaloriesLibraryContext context) : base(context)
        {
        }
    }
}