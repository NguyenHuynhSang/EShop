using AutoMapper;

using EShop.Server.Client.Dtos.Customer;
using EShop.Server.Client.Service;
using EShop.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
namespace EShop.Server.Client.Controller
{
    [Route("api/client/[controller]/[action]")]
    [ApiController]
    public class ClientCustomerController : ControllerBase
    {
        private readonly ICustomerService _authService;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;


        

        public ClientCustomerController(ICustomerService authService, IConfiguration config, IMapper mapper)
        {

            this._config = config;
            this._authService = authService;
            this._mapper = mapper;
        }

        [HttpPost("register")]
        public IActionResult Register(CustomerForRegisterDto Customer)
        {
            // validate request
            try
            {
                Customer.Username = Customer.Username.ToLower();
                
                if (_authService.UserExists(Customer.Username))
                {
                    return BadRequest("Tên người dùng đã tồn tại");
                }

                var userToCreate = new Customer()
                {
                    Username = Customer.Username,
                    Password = Customer.Password,
                    Phone = ""
                    
                };
                var newUser = _authService.Register(userToCreate);
                if (newUser != null)
                {
                    this._authService.SaveChange();
                }
                var userFromRepo = _authService.Login(Customer.Username.ToLower(), Customer.Password);

                var claims = new[]
               {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username),
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = cred,
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var userToReturn = _mapper.Map<CustomerForDetailDto>(userFromRepo);

                return Ok(new
                {
                    token = tokenHandler.WriteToken(token),
                    customer = userToReturn,
                });


                return StatusCode(201);
                // return Ok(newUser);
            }
            catch (Exception ex)
            {
               
                return NotFound(ex.ToString());

            }

        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(CustomerForLoginDto user)
        {
            try
            {
                var userFromRepo = _authService.Login(user.Username.ToLower(), user.Password);

                if (userFromRepo == null) return Unauthorized();

                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username),
                };
               


                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = cred,
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var userToReturn = _mapper.Map<CustomerForDetailDto>(userFromRepo);

                return Ok(new
                {
                    token = tokenHandler.WriteToken(token),
                    customer = userToReturn,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }


        [HttpGet]
        public ActionResult<CustomerForDetailDto> GetCustomerDetail()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
                throw;
            }



        }




    }
}
