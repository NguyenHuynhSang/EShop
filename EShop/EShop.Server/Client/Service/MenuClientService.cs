using EShop.Server.Data.Repository;
using EShop.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Server.Client.Service
{
    public interface IMenuClientService
    {
        public IEnumerable<Menu> GetMenu();
    }
    public class MenuClientService : IMenuClientService
    {
        
        private readonly IMenuRepository _menuRepository;

        public MenuClientService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }
        public IEnumerable<Menu> GetMenu()
        {
            var result = _menuRepository.GetMulti(x=>x.MenuGroupId==2 && x.IsActive==true);
            return result;
        }
    }
}
