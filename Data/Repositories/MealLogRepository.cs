using Data.Entities;
using Data.Web.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
