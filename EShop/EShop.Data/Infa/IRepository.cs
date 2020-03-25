using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EShop.Data.In
{

    /// <summary>
    ///  Chứa các phương thức dùng chung 
    /// </summary>
    public interface IRepository<T> where T : class
    {
        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void DeleteMulti(Expression<Func<T, bool>> where);

        T GetSingleById(int id);

        /// <summary>
        /// Lấy entity dựa vào điều kiện
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="includes"> vi du nhu may cai bảng con</param>
        /// <returns></returns>
        T GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null);

        IQueryable<T> GetAll(string[] include = null);

        IQueryable<T> GetMulti(Expression<Func<T, bool>> predicate, string[] include = null);

        IQueryable<T> GetMultiPaging(Expression<Func<T, bool>> filter
            , out int total, int index = 0, int size = 50, string[] include = null);

        int Count(Expression<Func<T, bool>> where);

        bool CheckContains(Expression<Func<T, bool>> predicate);

    }
}
