using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Dtos;
using EShop.Server.Models;
using EShop.Server.Repository;
using EShop.Server.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
namespace EShop.Server.Controllers
{
    public class AuthController : ApiControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, IConfiguration config, IMapper mapper)
        {

            this._config = config;
            this._authService = authService;
            this._mapper = mapper;
        }

        [HttpPost("register")]
        public IActionResult Register(UserForRegisterDto user)
        {
            // validate request
            try
            {
                user.Username = user.Username.ToLower();

                if (_authService.UserExists(user.Username))
                {
                    return BadRequest("User already exists");
                }

                var userToCreate = new User
                {
                    Username = user.Username,
                };
                var newUser = _authService.Register(userToCreate, user.Password);
                if (newUser != null)
                {
                    this._authService.SaveChange();
                }

                return StatusCode(201);
                // return Ok(newUser);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return NotFound();

            }
           
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto user)
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
                var userToReturn = _mapper.Map<UserForListDto>(userFromRepo);

                return Ok(new
                {
                    token = tokenHandler.WriteToken(token),
                    user = userToReturn,
                });
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return NotFound();
            }

        }
    }
}