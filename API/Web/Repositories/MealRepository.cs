using System.Linq;
using API.Web.DbContexts;
using API.Web.Entities;

namespace API.Web.Repositories
{
    public class MealRepository : GenericRepository<Meal>
    {
        public MealRepository(CaloriesLibraryContext context) : base(context)
        {
        }

        public override Meal Update(Meal entity)
        {
            var meal = _context.Meals.Single(m => m.Id == entity.Id);
            
            meal.MealElements = entity.MealElements;

            return base.Update(meal);
        } 
        
        public override Meal Delete(Meal entity)
        {
            var meal = _context.Meals.Single(m => m.Id == entity.Id);

            return base.Delete(meal);
        }
    }
}