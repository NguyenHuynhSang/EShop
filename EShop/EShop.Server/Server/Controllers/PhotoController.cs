﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using EShop.Server.Dtos.Admin;
using EShop.Server.Helper;
using EShop.Server.Server.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;

namespace EShop.Server.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private Cloudinary _cloudinary;
        private readonly IOptions<CloudinarySetting> _cloudinaryConfig;
        public PhotoController(IOptions<CloudinarySetting> cloudinaryConfig)
        {
            _cloudinaryConfig = cloudinaryConfig;

            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
                );

            _cloudinary = new Cloudinary(acc);

        }

        [HttpPost]
        public ActionResult<Photo> AddPhoto([FromForm] IFormFile File )
        {
            try
            {
                Photo photo = new Photo();
                var updateResult = new ImageUploadResult();
                var file = File;

                if (file.Length > 0)
                {
                    using (var stream = file.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(file.Name, stream)
                        };
                        updateResult = _cloudinary.Upload(uploadParams);
                    }
                }

                // xóa bỏ file khi đã lưu để giảm lưu lượng trả về
                photo.Url = updateResult.Url.ToString();
                photo.PublicId = updateResult.PublicId;
                return Ok(photo);
            }
            catch (Exception ex)
            {

                throw ex;
            }
    
        }


    }
}