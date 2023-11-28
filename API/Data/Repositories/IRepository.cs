using System.Linq.Expressions;

namespace Data.Repositories
{
    public interface IRepository<T>
    {
        T Add(T entity);
        T Update(T entity);
        T Get(int id);
        IEnumerable<T> All();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        T Delete(T entity);
        T Clone(T entity);
        void SaveChanges();
    }
}