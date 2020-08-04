using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace EShop.Server.Data
{
    /// <summary>
    /// Thân hàm mấy thằng phương thức chung
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RepositoryBase<T> : IRepository<T> where T : class, new()
    {
        private readonly EShopDbContext _dbContext;
        private readonly DbSet<T> dbSet;

 

        protected RepositoryBase(EShopDbContext dbContext)
        {
            _dbContext = dbContext;
            dbSet = _dbContext.Set<T>();
        }

        protected EShopDbContext DbContext
        {
            get { return _dbContext; }
        }

        public virtual T Add(T entity)
        {
            return dbSet.Add(entity).Entity;
        }

        public virtual void Update(T entity)
        {

            dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual T Delete(T entity)
        {
            return dbSet.Remove(entity).Entity;
        }

        public virtual void DeleteMulti(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbSet.Remove(obj);
        }

        public virtual T GetSingleById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual T GetSingleByCondition(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            if (include!=null)
            {
                var query = _dbContext.Set<T>();
              
                return include(query).FirstOrDefault(filter);
            }
            return _dbContext.Set<T>().FirstOrDefault(filter);
        }

        public virtual IEnumerable<T> GetAll(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (include != null )
            {
                var query = _dbContext.Set<T>();
                
                return include(query);
            }

            return _dbContext.Set<T>().AsQueryable();
        }

        public virtual IEnumerable<T> GetMulti(Expression<Func<T, bool>> filter,Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (include != null)
            {
                var query = _dbContext.Set<T>();
                return filter == null?include(query) : include(query.Where<T>(filter).AsQueryable<T>());
            }

            return filter == null ? _dbContext.Set<T>().AsQueryable() : _dbContext.Set<T>().Where<T>(filter).AsQueryable<T>();
        }

        public virtual IEnumerable<T> GetMultiPaging(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            int skipCount = index * size;
            IQueryable<T> _resetSet;

            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (include != null )
            {
                var query = _dbContext.Set<T>();
            
                _resetSet = filter != null ?  include(query).Where<T>(filter) : include(query);
            }
            else
            {
                _resetSet = filter != null ? _dbContext.Set<T>().Where<T>(filter).AsQueryable() : _dbContext.Set<T>().AsQueryable();
            }

            _resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.Skip(skipCount).Take(size);
            total = _resetSet.Count();
            return _resetSet.AsQueryable();
        }

        public int Count(Expression<Func<T, bool>> filter)
        {
            return dbSet.Count(filter);
        }

        public bool CheckContains(Expression<Func<T, bool>> filter)
        {
            return _dbContext.Set<T>().Count<T>(filter) > 0;
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

     
    }
}