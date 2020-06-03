using EShop.Data.DataCore;
using EShop.Data.Repository;
using EShop.EShop.Server.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Service.Service
{
    public interface ITagService
    {
        Tag Add(Tag Tag);
        IEnumerable<Tag> GetAll(string keyword);

        public Tag GetTagById(int id);

        public Tag Delete(Tag Tag);

        void SaveChanges();

    }
    public class TagService : ITagService
    {
        ITagRepository _TagRepository;
        IUnitOfWork _unitOfWork;

        public TagService(ITagRepository TagRepository, IUnitOfWork unitOfWork)
        {
            this._TagRepository = TagRepository;
            this._unitOfWork = unitOfWork;

        }
        public Tag Add(Tag Tag)
        {
            return _TagRepository.Add(Tag);
        }

        public Tag Delete(Tag Tag)
        {
            return _TagRepository.Delete(Tag);
        }

        public IEnumerable<Tag> GetAll(string keyword)
        {
            if (String.IsNullOrEmpty(keyword))
            {
                return _TagRepository.GetAll();
            }
            else
            {
                return _TagRepository.GetMulti(x => x.TagtName.Contains(keyword));
            }

        }


        public Tag GetTagById(int id)
        {
            return _TagRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
