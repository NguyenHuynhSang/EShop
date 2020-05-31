using EShop.Data.DataCore;
using EShop.Model.Models;
using EShop.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EShop.Data.Repository
{
    public interface IContentCategoryRepository:IRepository<ContentCategory>
    {
        public IEnumerable<ContentCategoryViewmodel> GetContentCategoriesForView(string Keyword);
        public ContentCategoryViewmodel GetContentCategoryByID(int id);

    }

    public class ContentCategoryRepository : RepositoryBase<ContentCategory>, IContentCategoryRepository
    {
        public ContentCategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
        public IEnumerable<ContentCategoryViewmodel> GetContentCategoriesForView(string Keyword)
        {
            if (string.IsNullOrEmpty(Keyword))
            {
                var querry = from n in DbContext.ContentCategories
                             join c in DbContext.ContentCategories
                             on n.ParentID equals c.ID into category
                             from nc in category.DefaultIfEmpty()
                             select new ContentCategoryViewmodel
                             {
                                 child = n,
                                 parent = nc == null ? null : nc,
                             };
                return querry.ToList();
            }
            else
            {
                var querry = from n in DbContext.ContentCategories
                             join c in DbContext.ContentCategories
                             on n.ParentID equals c.ID into category
                             from nc in category.DefaultIfEmpty()
                             where n.Name.Contains(Keyword) || nc.Name.Contains(Keyword)
                             select new ContentCategoryViewmodel
                             {
                                 child = n,
                                 parent = nc == null ? null : nc,
                             };
                return querry.ToList();
            }
        }
        public ContentCategoryViewmodel GetContentCategoryByID(int id)
        {
            var querry = from n in DbContext.ContentCategories
                         join c in DbContext.ContentCategories
                         on n.ParentID equals c.ID into category
                         from nc in category.DefaultIfEmpty()
                         where n.ID == id
                         select new ContentCategoryViewmodel
                         {
                             child = n,
                             parent = nc == null ? null : nc,
                         };
            return querry.FirstOrDefault();
        }
    }
}
