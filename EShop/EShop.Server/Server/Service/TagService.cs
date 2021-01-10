using EShop.Server.Data;
using EShop.Server.Repository;
using EShop.Server.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Server.Service
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
       

        public TagService(ITagRepository TagRepository)
        {
            this._TagRepository = TagRepository;
           

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
                return _TagRepository.GetMulti(null);
            }

        }


        public Tag GetTagById(int id)
        {
            return _TagRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _TagRepository.Commit();  
        }
    }
}
