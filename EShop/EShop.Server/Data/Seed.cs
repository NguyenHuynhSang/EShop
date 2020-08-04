using EShop.Server.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Data
{
    public class Seed
    {
        private readonly EShopDbContext _context;
        public Seed(EShopDbContext context)
        {
            _context = context;
              SeedUsers();

             SeedProduct();
        }

        private void SeedUsers()
        {
            var userData = File.ReadAllText("Data/user.data.json");
            var users = JsonConvert.DeserializeObject<List<User>>(userData);

            foreach (var user in users)
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash("password", out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.Username = user.Username.ToLower();

                _context.Users.Add(user);
            }

            _context.SaveChanges();
        }


        private void SeedProduct()
        {
            var productData = File.ReadAllText("Data/product.data.json");
            var products = JsonConvert.DeserializeObject<List<Product>>(productData);

            foreach (var product in products)
            {
                _context.Products.Add(product);
            }

            _context.SaveChanges();
        }



        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
