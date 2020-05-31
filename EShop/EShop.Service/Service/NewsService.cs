using EShop.Data.DataCore;
using EShop.Data.Repository;
using EShop.Model.Models;
using EShop.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Service.Service
{
    public interface INewsService
    {
        void Add(News news);
        IEnumerable<News> GetAll();

        public News GeNewsById(int id);

        void SaveChanges();

        IEnumerable<NewsViewmodel> GetNewsForView();

    }
  public  class NewsService : INewsService
    {
        INewsRepository _ProductRepository;
        IUnitOfWork _unitOfWork;

        public NewsService(INewsRepository newRepository, IUnitOfWork unitOfWork)
        {
            this._ProductRepository = newRepository;
            this._unitOfWork = unitOfWork;

        }
        public void Add(News news)
        {
            this._ProductRepository.Add(news);
        }

        public News GeNewsById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<News> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NewsViewmodel> GetNewsForView()
        {
            return _ProductRepository.GetNewsForView();
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
