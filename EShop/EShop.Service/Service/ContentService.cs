using EShop.Data.DataCore;
using EShop.Data.Repository;
using EShop.Model.Models;
using EShop.Model.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EShop.Service.Service
{
    public interface IContentService
    {
        Content Add(Content content);
        void SaveChanges();
        ContentViewmodel GetContentByID(int ID);
        public Content Delete(Content content);
        public Content Update(Content content);
        IEnumerable<ContentViewmodel> GetContentForView(string Keyword);
    }

    public class ContentService : IContentService
    {
        IContentRepository _contentRepository;
        IUnitOfWork _unitOfWork;
        ITagRepository _tagRepository;
        IContentTagRepository _contenttagRepository;
        DbContext context;
        public ContentService(IContentRepository contentRepository, IUnitOfWork unitOfWork,ITagRepository tagRepository,IContentTagRepository contentTagRepository)
        {
            _contentRepository = contentRepository;
            _unitOfWork = unitOfWork;
            _tagRepository = tagRepository;
            _contenttagRepository = contentTagRepository;
        }

        public Content Add(Content content)
        {
            Content ct = _contentRepository.Add(content);
            if (!string.IsNullOrEmpty(content.Tags))
            {
                string[] tags = content.Tags.Split(',');
                foreach (var tag in tags)
                {
                    var tagId = ToUnsignString(tag);
                    var existedTag = _tagRepository.CheckTag(tagId);

                    Tag _tag = new Tag();
                    _tag.TagID = tagId;
                    _tag.TagName = tagId;
                    //insert to to tag table
                    if (!existedTag)
                    {
                        _tagRepository.Add(_tag);
                    }

                    //insert to content tag
                    ContentTag _contenttag = new ContentTag();
                    _contenttag.TagID = tagId;
                    _contenttag.ContentID = content.ID;
                    _contenttagRepository.Add(_contenttag);

                }
            }
            return ct;
        }

        public IEnumerable<ContentViewmodel> GetContentForView(string Keyword)
        {
            return _contentRepository.GetContentsForView(Keyword);
        }

        public ContentViewmodel GetContentByID(int id)
        {
            return _contentRepository.GetContentByID(id);
        }

        public Content Delete(Content content)
        {
            return _contentRepository.Delete(content);

        }

        public Content Update(Content content)
        {
            _contentRepository.Update(content);
            if (!string.IsNullOrEmpty(content.Tags))
            {
                _contenttagRepository.RemoveAllContentTag(content.ID);
                string[] tags = content.Tags.Split(',');
                foreach (var tag in tags)
                {
                    var tagId = ToUnsignString(tag);
                    var existedTag = _tagRepository.CheckTag(tagId);

                    Tag _tag = new Tag();
                    _tag.TagID = tagId;
                    _tag.TagName = tagId;
                    //insert to to tag table
                    if (!existedTag)
                    {
                        _tagRepository.Add(_tag);
                    }

                    //insert to content tag
                    ContentTag _contenttag = new ContentTag();
                    _contenttag.TagID = tagId;
                    _contenttag.ContentID = content.ID;
                    _contenttagRepository.Add(_contenttag);

                }
            }
            SaveChanges();
            return content;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public static string ToUnsignString(string input)
        {
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            input = input.Replace(".", "-");
            input = input.Replace(" ", "-");
            input = input.Replace(",", "-");
            input = input.Replace(";", "-");
            input = input.Replace(":", "-");
            input = input.Replace("  ", "-");
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            while (str2.Contains("--"))
            {
                str2 = str2.Replace("--", "-").ToLower();
            }
            return str2;
        }

    }

}
