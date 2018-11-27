using CoreWebApp.Data.DBContext;
using CoreWebApp.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using CoreWebApp.Core;
using Z.EntityFramework.Plus;
using CoreWebApp.Model;
using System.Data.Common;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreWebApp.Repository
{
    public class Repository<T> : IRepository<T> where T: Entity
    {
        private DataContext _context;
        public Repository(DataContext context)
        {
            _context = context;
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> exp = null)
        {
            IQueryable<T> Filter(Expression<Func<T, bool>> expression)
            {
                var dbSet = _context.Set<T>().AsNoTracking().AsQueryable();
                if (exp != null)
                    dbSet = dbSet.Where(expression);
                return dbSet;
            }
            return Filter(exp);
        }

        public bool IsExist(Expression<Func<T, bool>> exp)
        {
            return _context.Set<T>().Any(exp);
        }

        /// <summary>
        /// 查找单个，且不被上下文所跟踪
        /// </summary>
        public T FindSingle(Expression<Func<T, bool>> exp)
        {
            return _context.Set<T>().AsNoTracking().FirstOrDefault(exp);
        }

        /// <summary>
        /// 得到分页记录
        /// </summary>
        /// <param name="pageindex">The pageindex.</param>
        /// <param name="pagesize">The pagesize.</param>
        /// <param name="orderby">排序，格式如："Id"/"Id descending"</param>
        public IQueryable<T> Find(int pageindex, int pagesize, string orderby = "", Expression<Func<T, bool>> exp = null)
        {

            IQueryable<T> Filter(Expression<Func<T, bool>> expression)
            {
                var dbSet = _context.Set<T>().AsNoTracking().AsQueryable();
                if (exp != null)
                    dbSet = dbSet.Where(expression);
                return dbSet;
            }

            if (pageindex < 1) pageindex = 1;
            if (string.IsNullOrEmpty(orderby))
                orderby = "Id descending";

            return Filter(exp).OrderBy(orderby).Skip(pagesize * (pageindex - 1)).Take(pagesize);
        }

        public int GetCount(Expression<Func<T, bool>> exp = null)
        {
            IQueryable<T> Filter(Expression<Func<T, bool>> expression)
            {
                var dbSet = _context.Set<T>().AsNoTracking().AsQueryable();
                if (exp != null)
                    dbSet = dbSet.Where(expression);
                return dbSet;
            }
            return Filter(exp).Count();
        }

        public void Insert(T entity)
        {
            if (string.IsNullOrEmpty(entity.Id))
            {
                entity.Id = Guid.NewGuid().ToString();
            }
            _context.Set<T>().Add(entity);
            Save();
            _context.Entry(entity).State = EntityState.Detached;
        }

        public void Insert(T[] entities)
        {
            foreach (var entity in entities)
            {
                entity.Id = Guid.NewGuid().ToString();
            }
            _context.Set<T>().AddRange(entities);
            Save();
        }

        public void Update(T entity)
        {
            var entry = this._context.Entry(entity);
            entry.State = EntityState.Modified;

            //如果数据没有发生变化
            if (!this._context.ChangeTracker.HasChanges())
            {
                return;
            }

            Save();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            Save();
        }

        /// <summary>
        /// 实现按需要只更新部分更新
        /// <para>如：Update(u =>u.Id==1,u =>new User{Name="ok"});</para>
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="entity">The entity.</param>
        public void Update(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity)
        {
            _context.Set<T>().Where(where).Update(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> exp)
        {
            _context.Set<T>().Where(exp).Delete();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public int ExecuteSql(string sql)
        {
            return _context.Database.ExecuteSqlCommand(sql);
        }

        public T FindBy(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }
        public IEnumerable<T> FindListBy(Expression<Func<T, bool>> predicate, int top)
        {
            if (top > 0)
            {
                return _context.Set<T>().AsNoTracking().Where(predicate).Take(top).ToArray();
            }
            return _context.Set<T>().AsNoTracking().Where(predicate).ToArray();
        }
        public bool Exists(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Count(predicate) > 0;
        }
        protected string TableName
        {
            get
            {
                var entityType = typeof(T);
                var tb = entityType.GetCustomAttribute<TableAttribute>();
                if (tb == null)
                {
                    throw new Exception($"无法获取表名，{entityType.Name}无TableAttribute特性。");
                }
                return tb.Name;
            }
        }
    }
}
