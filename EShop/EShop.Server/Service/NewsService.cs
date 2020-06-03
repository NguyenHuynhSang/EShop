﻿using EShop.Server.Data;
using EShop.Server.Repository;
using EShop.Server.Models;
using EShop.Server.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Server.Service
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
        INewsRepository _productRepository;
        IUnitOfWork _unitOfWork;

        public NewsService(INewsRepository newRepository, IUnitOfWork unitOfWork)
        {
            this._productRepository = newRepository;
            this._unitOfWork = unitOfWork;

        }
        public void Add(News news)
        {
            this._productRepository.Add(news);
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
            return _productRepository.GetNewsForView();
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
