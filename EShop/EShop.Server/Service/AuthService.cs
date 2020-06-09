using EShop.Server.Models;
using EShop.Server.Repository;
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

    }

  
    public class AuthService :  IAuthService
    {
        private IAuthRepository _authRepository;
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

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public User Login(string username, string password)
        {
            var user = _authRepository.GetSingleByConditionAddRelative(username,password);



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

       
    }
}
