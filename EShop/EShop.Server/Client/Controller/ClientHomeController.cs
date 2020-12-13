using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Server.Client.Service;
using EShop.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Server.Client.Controller
{
    [Route("api/client/[controller]/[action]")]
    [ApiController]
    public class ClientHomeController : ControllerBase
    {
        private readonly ISlideClientService _slideClientService;
        private readonly IMenuClientService _menuClientService;

        public ClientHomeController(ISlideClientService slideClientService, IMenuClientService menuClientService)
        {
            _slideClientService = slideClientService;
            _menuClientService = menuClientService;
        }



        [HttpGet]

        public ActionResult<IEnumerable<Slide>> GetSlide()
        {
            try
            {
                //TEST
                var slides = _slideClientService.GetAllSlide();
                return Ok(slides);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet]
        public ActionResult<Menu> GetMainMenu()
        {
            try
            {
                //TEST
                var menu = _menuClientService.GetMenu();
                return Ok(menu);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    

    }
}