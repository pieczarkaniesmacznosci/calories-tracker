using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using API.Web.DbContexts;

namespace API.Web.Repositories
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class
    {
        protected CaloriesLibraryContext _context;

        public GenericRepository(CaloriesLibraryContext context)
        {
            _context = context;
        }

        public virtual T Add(T entity)
        {
            return _context.Add(entity).Entity;
        }

        public virtual IEnumerable<T> All()
        {
            return _context.Set<T>().ToList();
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().AsQueryable<T>().Where(predicate).ToList();
        }

        public virtual T Get(int id)
        {
            return _context.Find<T>(id);
        }

        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }

        public virtual T Update(T entity)
        {
            return _context.Update(entity).Entity;
        }
        
        public virtual T Delete(T entity)
        {
            return _context.Remove(entity).Entity;
        }
    }
}