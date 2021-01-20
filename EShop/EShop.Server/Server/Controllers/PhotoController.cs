using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using EShop.Server.Dtos.Admin;
using EShop.Server.Extension;
using EShop.Server.Helper;
using EShop.Server.Models;
using EShop.Server.Repository;
using EShop.Server.Server.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace EShop.Server.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private Cloudinary _cloudinary;
        private readonly IOptions<CloudinarySetting> _cloudinaryConfig;
        private readonly ITagRepository _tagRepository;
        public PhotoController(IOptions<CloudinarySetting> cloudinaryConfig, ITagRepository tagRepository)
        {
            _cloudinaryConfig = cloudinaryConfig;

            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
                );

            _cloudinary = new Cloudinary(acc);
            _tagRepository = tagRepository;

        }


        [HttpPost]
        public async Task<ActionResult<IEnumerable<Tag>>> TaggingApi(IFormFile File)
        {
            var file = File;
            using (var httpClient = new HttpClient())
            {
                using (var form = new MultipartFormDataContent())
                {
                    using (var fs = file.OpenReadStream())
                    {
                        using (var streamContent = new StreamContent(fs))
                        {
                            using (var fileContent = new ByteArrayContent(await streamContent.ReadAsByteArrayAsync()))
                            {
                                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

                                // "file" parameter name should be the same as the server side input parameter name
                                form.Add(fileContent, "file", file.FileName);
                                var response = await httpClient.PostAsync(@"http://flask-api123.herokuapp.com/tagging", form);
                                var rs = response.Content;
                                string responseBody = await response.Content.ReadAsStringAsync();
                                TagReturn json = JsonConvert.DeserializeObject<TagReturn>(responseBody);
                               var result= _tagRepository.GetMulti(x => x.EnName == json.Category || x.EnName==json.Color);
                                return Ok(result);
                            }
                        }
                    }
                }
            }
        }

        [HttpPost]
        [SwaggerOperationCustom(Summary = "Lưu hình ảnh lên cloud", FileName = "photo_create.html")]
        public async Task<ActionResult<Entities.Photo>> AddPhotoAsync(IFormFile File)
        {
            try
            {
                Entities.Photo photo = new Entities.Photo();
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

                return BadRequest(ex);
            }

        }



        [HttpPost]
        [SwaggerOperationCustom(Summary = "Lưu hình ảnh lên cloud", FileName = "photo_create.html")]
        public ActionResult<Entities.Photo> AddPhotoTagging(IFormFile File)
        {
            try
            {
                Entities.Photo photo = new Entities.Photo();
                var updateResult = new ImageUploadResult();
                var file = File;

                if (file.Length > 0)
                {
                    using (var stream = file.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(file.Name, stream),
                            Categorization = "google_tagging",
                            AutoTagging = 0.9f
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

                return BadRequest(ex);
            }

        }

        [HttpPost]
        [SwaggerOperationCustom(Summary = "Lưu hình ảnh lên cloud", FileName = "photo_create.html")]
        public ActionResult<Entities.Photo> AddPhotoTaggingImma(IFormFile File)
        {
            try
            {
                Entities.Photo photo = new Entities.Photo();
                var updateResult = new ImageUploadResult();
                var file = File;

                if (file.Length > 0)
                {
                    using (var stream = file.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(file.Name, stream),
                            Categorization = "imagga_tagging",
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

                return BadRequest(ex);
            }

        }

    }
}