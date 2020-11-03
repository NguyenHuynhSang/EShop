using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EShop.Server.Data
{

    /// <summary>
    ///  Chứa các phương thức dùng chung 
    /// </summary>
    public interface IRepository<T> where T : class, new()
    {
        T Add(T entity);

      
        T Update(T entity);

        T Delete(T entity);


        void DeleteMulti(Expression<Func<T, bool>> filter);

        T GetSingleById(int id);

        /// <summary>
        /// Lấy entity dựa vào điều kiện
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includes"> vi du nhu may cai bảng con</param>
        /// <returns></returns>
        T GetSingleByCondition(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        IEnumerable<T> GetAll(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        IEnumerable<T> GetMulti(Expression<Func<T, bool>> filter , Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        IEnumerable<T> GetMultiPaging(Expression<Func<T, bool>> filter
            , out int total, int index = 0, int size = 50, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        int Count(Expression<Func<T, bool>> filter);

        bool CheckContains(Expression<Func<T, bool>> filter);

        public void ClearAllData();
        void Commit();

    }
}
