using EShop.Data.DataCore;
using EShop.Model.Models;
using EShop.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EShop.Data.Repository
{

    public interface INewsRepository : IRepository<News>
    {
        public IEnumerable<NewsViewmodel> GetNewsForView();
    }
    public class NewsRepository : RepositoryBase<News>, INewsRepository
    {

        public NewsRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
        public IEnumerable<NewsViewmodel> GetNewsForView()
        {
            var querry = from n in DbContext.News
                         join c in DbContext.Categories
                         on n.categoryID equals c.ID
                         select new NewsViewmodel
                         {
                             News = n,
                             Caterory = c,
                         };
            return querry.ToList();


        }
    }
}
