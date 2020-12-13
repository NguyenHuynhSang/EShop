using EShop.Server.Data.Repository;
using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Client.Service
{
    public interface ISlideClientService
    {
        public IEnumerable<Slide> GetAllSlide();
    }
    public class SlideClientService : ISlideClientService
    {

        private readonly ISlideRepository _slideRepository;

        public SlideClientService(ISlideRepository slideRepository)
        {
            _slideRepository = slideRepository;
        }

        public IEnumerable<Slide> GetAllSlide()
        {
            var result = _slideRepository.GetMulti(x=>x.IsActive==true);
            return result;


        }
    }
}
