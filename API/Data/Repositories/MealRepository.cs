using Data.DbContexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public class MealRepository : GenericRepository<Meal>
    {
        public MealRepository(CaloriesLibraryContext context) : base(context)
        {
        }

        public override Meal Get(int id)
        {
            return _context
                .Meals
                .AsQueryable()
                .Include(x => x.MealProducts)
                .ThenInclude(x => x.Product)
                .FirstOrDefault(x => x.Id == id);
        }

        public override IEnumerable<Meal> Find(Expression<Func<Meal, bool>> predicate)
        {
            return _context.Meals
                .AsQueryable()
                .Include(x => x.MealProducts)
                .ThenInclude(x => x.Product)
                .Where(predicate)
                .ToList();
        }

        public override IEnumerable<Meal> All()
        {
            return _context
                .Meals
                .Include(x => x.MealProducts)
                .ThenInclude(x => x.Product)
                .ToList();
        }

        public override Meal Update(Meal entity)
        {
            var meal = _context.Meals.Single(m => m.Id == entity.Id);

            meal.MealProducts = entity.MealProducts;

            return base.Update(meal);
        }

        public override Meal Delete(Meal entity)
        {
            var meal = _context.Meals.Single(m => m.Id == entity.Id);

            return base.Delete(meal);
        }
    }
}
