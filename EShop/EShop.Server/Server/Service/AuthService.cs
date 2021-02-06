using AutoMapper;
using EShop.Server.Extension;
using EShop.Server.Models;
using EShop.Server.Repository;
using EShop.Server.Server.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Service
{
    public interface IAuthService
    {
        User Register(User user, string password);
        User Login(string username, string password);
        bool UserExists(string username);

        IEnumerable<UserForListDto> GetAll(Params param);
        void SaveChange();

        User Create(User user, string password);
    }


    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }
            }

            return true;
        }

        public AuthService(IAuthRepository authRepository, IMapper mapper)
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }

        public User Login(string username, string password)
        {
            var user = _authRepository.GetSingleByConditionAddRelative(username, password);

            if (user == null) return null;
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public User Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;

            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            return _authRepository.Add(user);
        }

        public bool UserExists(string username)
        {
            return this._authRepository.CheckContains(x => x.Username == username);
        }

        public void SaveChange()
        {
            this._authRepository.Commit();
        }

        public IEnumerable<UserForListDto> GetAll(Params param)
        {
            var query = _authRepository.GetMulti(null);
            var result = query.Select(x => _mapper.Map<UserForListDto>(x));
            try
            {
                if (!String.IsNullOrEmpty(param.filterProperty) && !String.IsNullOrEmpty(param.filterValue))
                {
                    result = result.AsQueryable().WhereTo(param);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result.AsQueryable().Distinct().OrderByWithDirection(param.sortBy, param.sort);
        }

        public User Create(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            return _authRepository.Add(user);
        }
    }
}
