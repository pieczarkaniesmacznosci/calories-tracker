using API.Web.DbContexts;
using API.Web.Entities;

namespace API.Web.Repositories
{
    public class UserWeightRepository : GenericRepository<UserWeight>
    {
        public UserWeightRepository(CaloriesLibraryContext context) : base(context)
        {
        }
    }
}