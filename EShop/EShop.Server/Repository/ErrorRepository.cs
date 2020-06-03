using EShop.Server.Data;
using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Server.Repository
{

    public interface IErrorRepository : IRepository<Error>//Truyền kiểu đối tượng vào genegic
    {
        IEnumerable<Error> GetByAlias(string alias);
    }
    public class ErrorRepository : RepositoryBase<Error>, IErrorRepository
    {


        public ErrorRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
        /// Các câu truy vấn với csdl thực hiện tại class này 
        public IEnumerable<Error> GetByAlias(string alias)
        {
         
            throw new NotImplementedException();
        }
    }
}
