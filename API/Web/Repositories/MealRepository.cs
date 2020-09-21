using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using API.Web.DbContexts;
using API.Web.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Web.Repositories
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
                .Include(x=>x.MealProducts)
                .FirstOrDefault(x=>x.Id == id);
        } 

        public override IEnumerable<Meal> Find(Expression<Func<Meal, bool>> predicate)
        {
            return _context.Meals
                .Include(x=>x.MealProducts)
                .AsQueryable<Meal>()
                .Where(predicate);
        }

        public override IEnumerable<Meal> All()
        {
            return _context
                .Meals
                .Include(x=>x.MealProducts)
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