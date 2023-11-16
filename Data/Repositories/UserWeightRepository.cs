using Data.Entities;
using Data.Web.DbContexts;

namespace Data.Repositories
{
    public class UserWeightRepository : GenericRepository<UserWeight>
    {
        public UserWeightRepository(CaloriesLibraryContext context) : base(context)
        {
        }
    }
}
