using AutoMapper;
using EShop.Server.Extension;
using EShop.Server.Models;
using EShop.Server.Server.Dtos;
using EShop.Server.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static EShop.Server.Extension.FilterExtension;

namespace EShop.Server.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        public AccountController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserForListDto>> GetAllPaging(string filterProperty, FilterOperator filterOperator, FilterType filterType, string filterValue, string filterValue1, string sortBy = "id", SortType sort = SortType.desc, int page = 1, int perPage = 50)
        {

            try
            {
                Params param = new Params();
                param.sortBy = sortBy;
                param.sort = sort;
                param.perPage = perPage;
                param.page = page;
                param.filterProperty = filterProperty;
                param.filterOperator = filterOperator;
                param.filterValue1 = filterValue1;
                param.filterValue = filterValue;
                param.filterType = filterType;
                var list = _authService.GetAll(param);
                return Ok(PagedList<UserForListDto>.ToPagedList(list, page, perPage));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost]

        public ActionResult Create(UserForCreateDto user)
        {
            try
            {
                user.Username = user.Username.ToLower();

                if (_authService.UserExists(user.Username))
                {
                    return BadRequest("User already exists");
                }
                var newUser = _mapper.Map<User>(user);
                newUser.Created = DateTime.Now;

                var userReturn = _authService.Create(newUser, user.Password);
                if (userReturn != null)
                {
                    this._authService.SaveChange();
                }
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }


    }
}
