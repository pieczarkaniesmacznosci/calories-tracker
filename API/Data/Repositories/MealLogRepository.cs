using Data.DbContexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public class MealLogRepository : GenericRepository<MealLog>
    {

        public MealLogRepository(CaloriesLibraryContext context) : base(context)
        {
        }

        public override MealLog Get(int id)
        {
            return _context
                .MealLogs
                .AsQueryable()
                .Include(x => x.Meal)
                .ThenInclude(x => x.MealProducts)
                .ThenInclude(x => x.Product)
                .FirstOrDefault(x => x.Id == id);
        }
        public override IEnumerable<MealLog> All()
        {
            return _context
                .MealLogs
                .AsQueryable()
                .Include(x => x.Meal)
                .ThenInclude(x => x.MealProducts)
                .ThenInclude(x => x.Product)
                .ToList();
        }

        public override IEnumerable<MealLog> Find(Expression<Func<MealLog, bool>> predicate)
        {
            return _context
                .MealLogs
                .Include(x => x.Meal)
                .ThenInclude(x => x.MealProducts)
                .ThenInclude(x => x.Product)
                .Where(predicate)
                .ToList();
        }
    }
}
