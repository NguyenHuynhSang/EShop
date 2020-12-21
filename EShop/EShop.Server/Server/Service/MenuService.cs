using EShop.Server.Data.Repository;
using EShop.Server.Extension;
using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Server.Service
{

    public interface IMenuService
    {
        public Menu Add(Menu menu);
        public Menu Update(Menu menu);
        public IEnumerable<Menu> GetAll(Params param);

        public Menu Delete(int id);

        void SaveChanges();
    }
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }
        public Menu Add(Menu menu)
        {
            menu.CreatedDate = DateTime.Now;
            return _menuRepository.Add(menu);
        }

        public Menu Delete(int id)
        {
            var menu = _menuRepository.GetSingleById(id);
            return _menuRepository.Delete(menu);
        }

        public IEnumerable<Menu> GetAll(Params param)
        {
            var query = _menuRepository.GetAll();
            return query;
        }

        public void SaveChanges()
        {
            _menuRepository.Commit();
        }

        public Menu Update(Menu menu)
        {
            menu.ModifiedDate = DateTime.Now;
            return _menuRepository.Update(menu);
        }
    }
}
