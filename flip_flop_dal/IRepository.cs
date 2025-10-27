using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace flip_flop_dal
{
    public interface IRepository<T> where T : class
    {
        void Add(IEnumerable<T> entitylist);
        void Update(T entity);
        void Add(T entity);
        void Delete(IEnumerable<T> entitylist);
        void Delete(T entity);
        IEnumerable<T> FetchAll();
        T FetchByPrimaryKey(object primarykey);
        IEnumerable<T> FindWhere(Expression<Func<T, bool>> predicate);
        T FirstOrDefault(Expression<Func<T, bool>> predicate);
    }
}
