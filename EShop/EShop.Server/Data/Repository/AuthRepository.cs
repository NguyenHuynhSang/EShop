using EShop.Server.Data;
using EShop.Server.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Repository
{
    public interface IAuthRepository : IRepository<User>
    {
        User GetSingleByConditionAddRelative(string username, string password);
        
    }
    public class AuthRepository : RepositoryBase<User>, IAuthRepository
    {

        public AuthRepository(EShopDbContext context) : base(context)
        {

        }

        public  User GetSingleByConditionAddRelative(string username, string password)
        {
            var user =  DbContext.Users.Include(u => u.Photos).FirstOrDefault(u => u.Username == username);
 
            return user;
        }

       

     

      
    
    }
}
