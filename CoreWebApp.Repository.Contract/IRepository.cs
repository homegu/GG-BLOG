using CoreWebApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CoreWebApp.Repository.Contract
{
    public interface IRepository<T> 
        where T : Entity
    {
        T FindSingle(Expression<Func<T, bool>> exp = null);
        bool IsExist(Expression<Func<T, bool>> exp);
        IQueryable<T> Find(Expression<Func<T, bool>> exp = null);

        IQueryable<T> Find(int pageindex = 1, int pagesize = 10, string orderby = "",
            Expression<Func<T, bool>> exp = null);

        int GetCount(Expression<Func<T, bool>> exp = null);

        void Insert(T entity);

        void Insert(T[] entities);

        void Update(T entity);

        void Delete(T entity);

        void Update(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity);

        void Delete(Expression<Func<T, bool>> exp);

        void Save();

        int ExecuteSql(string sql);

        IEnumerable<T> FindListBy(Expression<Func<T, bool>> predicate, int top);
        T FindBy(Expression<Func<T, bool>> predicate);
        bool Exists(Expression<Func<T, bool>> predicate);
    }
}
