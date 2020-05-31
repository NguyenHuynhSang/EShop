using EShop.Data.DataCore;
using EShop.Data.Repository;
using EShop.Model.Models;
using EShop.Model.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Service.Service
{
    public interface IContentCategoryService
    {
        ContentCategory Add(ContentCategory category); 
        void SaveChanges();
        ContentCategoryViewmodel GetContentCategoryByID(int ID);
        public ContentCategory Delete(ContentCategory category);
        public ContentCategory Update(ContentCategory category);
        IEnumerable<ContentCategoryViewmodel> GetContentCategoryForView(string Keyword);
    }

    public class ContentCategoryService : IContentCategoryService
    {
        IContentCategoryRepository _categoryRepository;
        IUnitOfWork _unitOfWork;
        DbContext context;    
        public ContentCategoryService(IContentCategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public ContentCategory Add(ContentCategory category)
        {
            return _categoryRepository.Add(category);
        }

        public IEnumerable<ContentCategoryViewmodel> GetContentCategoryForView(string Keyword)
        {
            return _categoryRepository.GetContentCategoriesForView(Keyword);
        }

        public ContentCategoryViewmodel GetContentCategoryByID(int id)
        {
            return _categoryRepository.GetContentCategoryByID(id);
        }

        public ContentCategory Delete(ContentCategory category)
        {
            return _categoryRepository.Delete(category);

        }

        public ContentCategory Update(ContentCategory category)
        {
            _categoryRepository.Update(category);
            SaveChanges();
            return category;
        }   

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
    
}
