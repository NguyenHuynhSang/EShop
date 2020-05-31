using EShop.Data.DataCore;
using EShop.Data.Repository;
using EShop.Model.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Service.Service
{
    public interface IContentTagService
    {
        ContentTag Add(ContentTag ContentTag);

        public ContentTag GetContentTagById(int id);

        public ContentTag Delete(ContentTag ContentTag);

        void SaveChanges();

    }
    public class ContentTagService : IContentTagService
    {
        IContentTagRepository _ContentTagRepository;
        IUnitOfWork _unitOfWork;

        public ContentTagService(IContentTagRepository ContentTagRepository, IUnitOfWork unitOfWork)
        {
            this._ContentTagRepository = ContentTagRepository;
            this._unitOfWork = unitOfWork;

        }
        public ContentTag Add(ContentTag ContentTag)
        {
            return _ContentTagRepository.Add(ContentTag);
        }

        public ContentTag Delete(ContentTag ContentTag)
        {
            return _ContentTagRepository.Delete(ContentTag);
        }


        public ContentTag GetContentTagById(int id)
        {
            return _ContentTagRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
