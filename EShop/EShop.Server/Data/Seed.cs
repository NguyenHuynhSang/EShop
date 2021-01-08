using AutoMapper;
using EShop.Server.Migrations;
using EShop.Server.Models;
using GHNApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace EShop.Server.Data
{
    public class Seed
    {
        private readonly EShopDbContext _context;
        private readonly IMapper _mapper;
        public Seed(EShopDbContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
            //CleanAllData();

            //   SeedUsers();
            //SeedProductAttribute();
            //SeedAttributeValue();
            //SeedProductCatalog();

            //if (_context.SeedLogs.Count() == 0)
            //{



            //    SeedLog log = new SeedLog();
            //    log.DataVersion = 1;
            //    context.SeedLogs.Add(log);
            //    context.SaveChanges();
            //}
            //else
            //{

            //}
         //   SeedDistrict();
            //SeedWard();
            if (_context.SeedLogs.Count() == 0)
            {

                SeedLog log = new SeedLog();
                log.DataVersion = 1;
                context.SeedLogs.Add(log);
                context.SaveChanges();
                SeedProvince();
             
                
            }
         



        }


        private void CleanAllData()
        {


            _context.RemoveRange(_context.Products);

            _context.SaveChanges();
        }




        private void SeedUsers()
        {
            var userData = File.ReadAllText("Data/Seed/user.data.json");
            var users = JsonConvert.DeserializeObject<List<User>>(userData);

            foreach (var user in users)
            {
                //prevent dumplicate data when seeding
                var count = _context.Users.Count(x => x.Username == user.Username);
                if (count > 0)
                {
                    return;
                }
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash("password", out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.Username = user.Username.ToLower();

                _context.Users.Add(user);
            }

            _context.SaveChanges();
        }





        private void SeedProductCatalog()
        {
            var dataJson = File.ReadAllText("Data/Seed/product.catalog.data.json");
            var data = JsonConvert.DeserializeObject<List<Models.ProductCatalog>>(dataJson);

            foreach (var item in data)
            {
                var count = _context.ProductCatalogs.Count(x => x.Id == item.Id);
                if (count > 0)
                {
                    _context.ProductCatalogs.AddOrUpdate(item);
                }
                else
                {
                    // item.Id = 0;// set lại giá trị cho Id
                    _context.ProductCatalogs.Add(item);
                }

            }

            _context.SaveChanges();


        }

        private void SeedAttributeValue()
        {
            var dataJson = File.ReadAllText("Data/Seed/attribute.value.data.json");
            var data = JsonConvert.DeserializeObject<List<Models.AttributeValue>>(dataJson);

            foreach (var item in data)
            {
                _context.AttributeValues.AddOrUpdate(item);

            }

            _context.SaveChanges();


        }


        private void SeedProvince()
        {
            var dataJson = File.ReadAllText("Data/Seed/province.data.json");
            var data = JsonConvert.DeserializeObject<List<Province>>(dataJson);

            foreach (var item in data)
            {
                _context.Provinces.AddOrUpdate(item);

            }
            _context.SaveChanges();
        }


        private void SeedDistrict()
        {
            var list = _context.District;
            _context.District.RemoveRange(list);
            _context.SaveChanges();
            var dataJson = File.ReadAllText("Data/Seed/district.data.json");
            var data = JsonConvert.DeserializeObject<List<District>>(dataJson);

            _context.District.AddRange(data);
            _context.SaveChanges();
            //int count = 0;
            //foreach (var item in data)
            //{
            //    if (count==359)
            //    {
            //        count = 0;

            //    }
            //    count++;
            //    _context.Add(item);
            //    _context.SaveChanges();

            




        }


        private void SeedWard()
        {
            var list = _context.Ward;
            _context.Ward.RemoveRange(list);
            _context.SaveChanges();
            var dataJson = File.ReadAllText("Data/Seed/ward.data.json");

            var dataJson2 = File.ReadAllText("Data/Seed/district.data.json");
            var data2 = JsonConvert.DeserializeObject<List<District>>(dataJson2);

            var db = _context.District;
            var data = JsonConvert.DeserializeObject<List<Ward>>(dataJson);
            int counter = 1;
         
            _context.Ward.AddRange(data);
            _context.SaveChanges();
           
          
          
        }




        private void SeedProduct()
        {
            var productData = File.ReadAllText("Data/Seed/product.data.json");
            var products = JsonConvert.DeserializeObject<List<Product>>(productData);

            foreach (var product in products)
            {


         
                _context.Products.Add(product);

                _context.SaveChanges();

            }


        }


        private void SeedProductAttribute()
        {
            var dataJson = File.ReadAllText("Data/Seed/product.attribute.data.json");
            var data = JsonConvert.DeserializeObject<List<Models.Attribute>>(dataJson);

            foreach (var item in data)
            {
                _context.Attributes.AddOrUpdate(item);

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

    public static class DbSetExtension
    {
        public static void AddOrUpdate<T>(this DbSet<T> dbSet, T data) where T : class
        {
            var context = dbSet.GetContext();
            var ids = context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.Select(x => x.Name);

            var t = typeof(T);
            List<PropertyInfo> keyFields = new List<PropertyInfo>();

            foreach (var propt in t.GetProperties())
            {
                var keyAttr = ids.Contains(propt.Name);
                if (keyAttr)
                {
                    keyFields.Add(propt);
                }
            }
            if (keyFields.Count <= 0)
            {
                throw new Exception($"{t.FullName} does not have a KeyAttribute field. Unable to exec AddOrUpdate call.");
            }
            var entities = dbSet.AsNoTracking().ToList();
            foreach (var keyField in keyFields)
            {
                var keyVal = keyField.GetValue(data);
                entities = entities.Where(p => p.GetType().GetProperty(keyField.Name).GetValue(p).Equals(keyVal)).ToList();
            }
            var dbVal = entities.FirstOrDefault();
            if (dbVal != null)
            {
                context.Entry(dbVal).CurrentValues.SetValues(data);
                context.Entry(dbVal).State = EntityState.Modified;
                return;
            }
            dbSet.Add(data);
        }

        public static void AddOrUpdate<T>(this DbSet<T> dbSet, Expression<Func<T, object>> key, T data) where T : class
        {
            var context = dbSet.GetContext();
            var ids = context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.Select(x => x.Name);
            var t = typeof(T);
            var keyObject = key.Compile()(data);
            PropertyInfo[] keyFields = keyObject.GetType().GetProperties().Select(p => t.GetProperty(p.Name)).ToArray();
            if (keyFields == null)
            {
                throw new Exception($"{t.FullName} does not have a KeyAttribute field. Unable to exec AddOrUpdate call.");
            }
            var keyVals = keyFields.Select(p => p.GetValue(data));
            var entities = dbSet.AsNoTracking().ToList();
            int i = 0;
            foreach (var keyVal in keyVals)
            {
                entities = entities.Where(p => p.GetType().GetProperty(keyFields[i].Name).GetValue(p).Equals(keyVal)).ToList();
                i++;
            }
            if (entities.Any())
            {
                var dbVal = entities.FirstOrDefault();
                var keyAttrs =
                    data.GetType().GetProperties().Where(p => ids.Contains(p.Name)).ToList();
                if (keyAttrs.Any())
                {
                    foreach (var keyAttr in keyAttrs)
                    {
                        keyAttr.SetValue(data,
                            dbVal.GetType()
                                .GetProperties()
                                .FirstOrDefault(p => p.Name == keyAttr.Name)
                                .GetValue(dbVal));
                    }
                    context.Entry(dbVal).CurrentValues.SetValues(data);
                    context.Entry(dbVal).State = EntityState.Modified;
                    return;
                }
            }
            dbSet.Add(data);
        }
    }

    public static class HackyDbSetGetContextTrick
    {
        public static DbContext GetContext<TEntity>(this DbSet<TEntity> dbSet)
            where TEntity : class
        {
            return (DbContext)dbSet
                .GetType().GetTypeInfo()
                .GetField("_context", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(dbSet);
        }
    }

}
