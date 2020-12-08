using System.Collections.Generic;
using System.Linq;
using API.Web.DbContexts;
using API.Web.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Web.Repositories
{
    public class MealLogRepository : GenericRepository<MealLog>
    {
        
        public MealLogRepository(CaloriesLibraryContext context) : base(context)
        {
        }

        public override IEnumerable<MealLog> All()
        {
            return _context
                .MealLogs
                .AsQueryable()
                .Include(x => x.Meal)
                .ThenInclude(x => x.MealProducts)
                .ThenInclude(x=>x.Product)
                .ToList();
        }
    }
}