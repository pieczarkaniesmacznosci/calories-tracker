using Data.DbContexts;
using Data.Entities;

namespace Data.Repositories
{
    public class UserWeightRepository : GenericRepository<UserWeight>
    {
        public UserWeightRepository(CaloriesLibraryContext context) : base(context)
        {
        }
    }
}
