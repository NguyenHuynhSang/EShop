using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Data.Repository
{
    public interface ISlideRepository : IRepository<Slide>
    {


    }

    public class SlideRepository : RepositoryBase<Slide>, ISlideRepository
    {


        public SlideRepository(EShopDbContext dbContext) : base(dbContext)
        {

        }


    }
}
