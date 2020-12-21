using EShop.Server.Models;
using EShop.Server.Server.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpPut]
        public ActionResult<Menu> Update(Menu menu)
        {
            try
            {
                //TEST
                var updatedmenu = _menuService.Update(menu);
                _menuService.SaveChanges();
                return Ok(updatedmenu);
            }
            catch (Exception ex)
            {
            
                return BadRequest(ex.ToString());
            }
        }


        [HttpGet]
        public ActionResult<IEnumerable<Menu>> GetAll()
        {
            try
            {
                //TEST
                var result = _menuService.GetAll(null);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        public ActionResult<Menu> Create(Menu menu)
        {
            try
            {
                //TEST
                var updatedmenu = _menuService.Add(menu);
                _menuService.SaveChanges();
                return Ok(updatedmenu);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
        }





        [HttpDelete("{id}")]
        public ActionResult<Menu> Delete(int id)
        {
            try
            {
                var oldEntity = _menuService.Delete(id);
                _menuService.SaveChanges();
                return oldEntity;
            }
            catch (Exception ex)
            {
              
                return NotFound();
            }
        }



    }
}
