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
using Microsoft.AspNetCore.Authorization;
using EShop.Server.Client.Dtos.Shipping;
using EShop.Server.Extension;
using Swashbuckle.AspNetCore.Annotations;

namespace EShop.Server.Client.Controller
{
    [Authorize]
    [Route("api/client/[controller]/[action]")]
    [ApiController]
    public class ClientCustomerController : ControllerBase
    {
        private readonly ICustomerService _authService;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IAddressService _addressService;

        

        public ClientCustomerController(ICustomerService authService, IConfiguration config, IMapper mapper, IAddressService addressService)
        {

            this._config = config;
            this._authService = authService;
            this._mapper = mapper;
            _addressService = addressService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
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

        [HttpGet]
        [SwaggerOperation(Summary ="AUTHEN")]
        public ActionResult<IEnumerable<AddressForUpdate>> GetAddresses(int userId)
        {
            try
            {
                var record= _addressService.GetAddress(userId);
                var result= record.Select(x => _mapper.Map<AddressForUpdate>(x));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
          
        }

        [HttpPost("login")]
        [AllowAnonymous]
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
                    Expires = DateTime.Now.AddDays(30),
                    SigningCredentials = cred,
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var userToReturn = _mapper.Map<CustomerForDetailDto>(userFromRepo);

                return Ok(new
                {
                    token = tokenHandler.WriteToken(token),
                    user = userToReturn,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        [HttpPost]
        [SwaggerOperation(Summary = "AUTHEN")]
        public ActionResult<CustomerForDetailDto> AddAddress(int userId,AddressForInputDto address)
        {
            try
            {
                var adr = _mapper.Map<Address>(address);
                adr.CustomerId = userId;
                var result = _addressService.AddAddress(adr);
                var user= _authService.GetCustomerById(userId);
                var userReturn = _mapper.Map<CustomerForDetailDto>(user);
                return Ok(userReturn);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
                throw;
            }
           



        }

        [HttpDelete]
        [SwaggerOperation(Summary = "AUTHEN")]
        [AllowAnonymous]
        public ActionResult<CustomerForDetailDto> DeleteAddress(int  addressId)
        {
            try
            {
                var result = _addressService.DeleteAddress(addressId);
                var user = _authService.GetCustomerById(result.CustomerId);
                var userReturn = _mapper.Map<CustomerForDetailDto>(user);
                return Ok(userReturn);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
                throw;
            }

        }

        [HttpPost]
        [SwaggerOperation(Summary = "AUTHEN")]
        public ActionResult<CustomerForDetailDto> SetMainAddress(int addressId)
        {
            try
            {
                var result = _addressService.SetMainAddress(addressId);
                var user = _authService.GetCustomerById(result.CustomerId);
                var userReturn = _mapper.Map<CustomerForDetailDto>(user);
                return Ok(userReturn);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
                throw;
            }

        }



        [HttpPost]
        [SwaggerOperation(Summary = "AUTHEN")]
        public async Task<IActionResult> UpdateInfor(CustomerForUpdateDto user)
        {
            try
            {
                var current = _authService.GetCustomerById(user.Id);
                if (!String.IsNullOrEmpty(user.CurrentPass))
                {
                    if (user.CurrentPass != current.Password)
                    {
                        return BadRequest(false);
                    }
                }
                else
                {
                    user.Password = current.Password;
                }
                var entity= _authService.UpdateInfor(user);
                 
                _authService.SaveChange();
                var userReturn = _mapper.Map<CustomerForDetailDto>(entity);
                return Ok(userReturn);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }


        [HttpGet]
        [SwaggerOperation(Summary = "AUTHEN")]
        public ActionResult<CustomerForDetailDto> GetCustomerDetail(int id)
        {
            try
            {
               var entity= _authService.GetCustomerById(id);
                var result=_mapper.Map<CustomerForDetailDto>(entity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
                throw;
            }



        }




    }
}
