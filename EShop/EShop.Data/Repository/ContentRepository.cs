using EShop.Data.DataCore;
using EShop.Model.Models;
using EShop.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EShop.Data.Repository
{
    public interface IContentRepository : IRepository<Content>
    {
        public IEnumerable<ContentViewmodel> GetContentsForView(string Keyword);
        public ContentViewmodel GetContentByID(int id);

    }

    public class ContentRepository : RepositoryBase<Content>, IContentRepository
    {
        public ContentRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
        public IEnumerable<ContentViewmodel> GetContentsForView(string Keyword)
        {
            if (string.IsNullOrEmpty(Keyword))
            {
                var querry = from n in DbContext.Contents
                             join c in DbContext.ContentCategories
                             on n.CategoryID equals c.ID into category
                             from nc in category.DefaultIfEmpty()
                             select new ContentViewmodel
                             {
                                 content = n,
                                 category = nc == null ? null : nc,
                             };
                return querry.ToList();
            }
            else
            {
                var querry = from n in DbContext.Contents
                             join c in DbContext.ContentCategories
                             on n.CategoryID equals c.ID into category
                             from nc in category.DefaultIfEmpty()
                             where n.Name.Contains(Keyword) || nc.Name.Contains(Keyword)
                             select new ContentViewmodel
                             {
                                 content = n,
                                 category = nc == null ? null : nc,
                             };
                return querry.ToList();
            }
        }
        public ContentViewmodel GetContentByID(int id)
        {
            var querry = from n in DbContext.Contents
                         join c in DbContext.ContentCategories
                         on n.CategoryID equals c.ID into category
                         from nc in category.DefaultIfEmpty()
                         where n.ID == id
                         select new ContentViewmodel
                         {
                             content = n,
                             category = nc == null ? null : nc,
                         };
            return querry.FirstOrDefault();
        }
    }
}
